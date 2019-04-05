using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace WebAnchor.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ReturnTypeShouldBeTaskAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "ReturnTypeShouldBeTaskAnalyzer";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md for more on localization
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "WebAnchor";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }
        
        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationStartAction(compilationContext =>
            {
                // Search Meziantou.SampleType
                var typeSymbol = compilationContext.Compilation.GetTypeByMetadataName("WebAnchor.Attributes.URL.HttpAttribute");
                if (typeSymbol == null)
                    return;

                // register the analyzer on Method symbol
                compilationContext.RegisterSymbolAction(AnalyzeMethod, SymbolKind.Method);
            });
        }
        private void AnalyzeMethod(SymbolAnalysisContext context)
        {
            INamedTypeSymbol TaskOfT = context.Compilation.GetTypeByMetadataName("System.Threading.Tasks.Task`1");
            INamedTypeSymbol Task = context.Compilation.GetTypeByMetadataName("System.Threading.Tasks.Task");
            INamedTypeSymbol HttpAttribute = context.Compilation.GetTypeByMetadataName("WebAnchor.Attributes.URL.HttpAttribute");
            var methodSymbol = (IMethodSymbol)context.Symbol;
            if (methodSymbol.GetAttributes().Any(x => IsOrInheritsFrom(x.AttributeClass, HttpAttribute)))
            {
                var returnType = methodSymbol.ReturnType as ITypeSymbol;
                if (returnType != null)
                {
                    if (!(IsOrInheritsFrom(returnType, Task) || IsOrInheritsFrom(returnType, TaskOfT)))
                    {
                        var node = methodSymbol.DeclaringSyntaxReferences[0].GetSyntax() as MethodDeclarationSyntax;
                        if (node != null)
                        {
                            var location = Location.Create(methodSymbol.DeclaringSyntaxReferences[0].SyntaxTree, node.ReturnType.Span);
                            var diagnostic = Diagnostic.Create(Rule, location, returnType.ToString());
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }

        private static bool IsOrInheritsFrom(ITypeSymbol symbol, ITypeSymbol type)
        {
            var baseType = symbol;
            while (baseType != null)
            {
                if (type.Equals(baseType))
                    return true;

                baseType = baseType.BaseType;
            }

            return false;
        }
    }
}
