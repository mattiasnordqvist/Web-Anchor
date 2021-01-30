using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace WebAnchor.Generators
{
    [Generator]
    public class ApiGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif 
            context.RegisterForSyntaxNotifications(() => new FindApisSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            Dictionary<ITypeSymbol, (List<string>, List<IMethodSymbol>)> interfaces = new Dictionary<ITypeSymbol, (List<string>, List<IMethodSymbol>)>();
            var attributeSymbol = context.Compilation.GetTypeByMetadataName("WebAnchor.Attributes.URL.HttpAttribute");
            var nodes = ((FindApisSyntaxReceiver)context.SyntaxReceiver).Nodes;
            foreach (InterfaceDeclarationSyntax candidate in nodes)
            {
                SemanticModel model = context.Compilation.GetSemanticModel(candidate.SyntaxTree);
              
                var root = candidate.SyntaxTree.GetRoot();

                List<string> usings = new List<string>();
                foreach (var usingDirective in root.DescendantNodes().OfType<UsingDirectiveSyntax>())
                {
                    var symbol = model.GetSymbolInfo(usingDirective.Name).Symbol;
                    var name = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    usings.Add(name);
                }

                ITypeSymbol interfaceSymbol = model.GetDeclaredSymbol(candidate) as ITypeSymbol;
                foreach (MemberDeclarationSyntax b in candidate.Members)
                {
                    IMethodSymbol methodSymbol = model.GetDeclaredSymbol(b) as IMethodSymbol;
                    if (methodSymbol.GetAttributes().Any(ad => ad.AttributeClass.BaseType.Equals(attributeSymbol, SymbolEqualityComparer.Default)))
                    {
                        if (!interfaces.ContainsKey(interfaceSymbol))
                        {
                            interfaces.Add(interfaceSymbol, (usings, new List<IMethodSymbol>()));
                        }

                        interfaces[interfaceSymbol].Item2.Add(methodSymbol);
                    }
                }
            }

            foreach (var i in interfaces)
            {
                var namespaceName = i.Key.ContainingNamespace;
                var apiName = i.Key.Name.TrimStart('I') + "Implementation"+Guid.NewGuid().ToString().Substring(0, 8);
                var source =
$@"{string.Join(Environment.NewLine, i.Value.Item1.Union(new[] { namespaceName.ToString(), "System", "System.Reflection", "WebAnchor" }).Select(x => $"using {x};"))}

namespace {namespaceName}
{{
    public class {apiName}{Regex.Match(i.Key.ToString(), "<.*>").Groups[0].Value} : {i.Key} 
    {{
        private Anchor _anchor;

        public {apiName} (Anchor anchor) => _anchor = anchor;                           
                                
        {BuildMethods(i.Key.ToString(), i.Value.Item2)}

        public void Dispose() 
        {{
            _anchor.Dispose();
        }}
    }} 
}}";
                var hintName = "WebAnchor.Generated."+i.Key.ToString() + ".implementation";
                foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                {
                    hintName = hintName.Replace(c, '_');
                }
                context.AddSource(
                    hintName,
                    SourceText.From(source, Encoding.UTF8)
                );
            }
        }

        private string BuildMethods(string apiName, List<IMethodSymbol> value)
        {
            return string.Join(Environment.NewLine, value.Select(x => BuildMethod(apiName, x)));
        }

        private string BuildMethod(string apiName, IMethodSymbol methodSymbol)
        {
            var returnType = methodSymbol.ReturnType.ToString();
            var match = Regex.Match(returnType, "System.Threading.Tasks.Task<(.*)>");
            string returnGenericParam;
            if (!match.Success)
            {
                returnGenericParam = null;
            }
            else
            {
                returnGenericParam = match.Groups[1].Value;
            }
            var methodName = methodSymbol.Name;
            var parameters = string.Join(", ",methodSymbol.Parameters.Select(x => x.Type+ " " +x.Name));
            var parameterValues = "new object[]{" + string.Join(", ", methodSymbol.Parameters.Select(x => x.Name)) + "}";
            var parameterTypes = "new Type[]{" + string.Join(", ", methodSymbol.Parameters.Select(x => "typeof("+x.Type+")")) + "}";
            return $@"
        public async {returnType} {methodName} ({parameters}) 
        {{
            var methodInfo = typeof({apiName}).GetMethod(""{methodName}"", {parameterTypes});
            var parameters = {parameterValues};
            { (returnGenericParam == null ? "await _anchor.Intercept<object>(methodInfo, parameters);" :  $"return await _anchor.Intercept<{returnGenericParam}>(methodInfo, parameters);") }
        }}";
        }
    }

    internal class FindApisSyntaxReceiver : ISyntaxReceiver
    {
        public List<InterfaceDeclarationSyntax> Nodes { get; private set; } = new List<InterfaceDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is InterfaceDeclarationSyntax a)
            {
                Nodes.Add(a);
            }
        }
    }
}
