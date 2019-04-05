using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace WebAnchor.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class WebAnchorAnalyzer : DiagnosticAnalyzer
    {
        public const string ReturnTypeShouldBeTaskDiagnosticId = "ReturnTypeShouldBeTaskDiagnosticId";
        public const string GetOrDeleteCantHaveContentDiagnosticId = "GetOrDeleteCantHaveContentDiagnosticId";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "WebAnchor";

        private static DiagnosticDescriptor ReturnTypeShouldBeTaskRule = new DiagnosticDescriptor(ReturnTypeShouldBeTaskDiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);
        private static DiagnosticDescriptor GetOrDeleteCantHaveContentRule = new DiagnosticDescriptor(GetOrDeleteCantHaveContentDiagnosticId, "Some http methods does not support Content", "{0} can't have Content", Category, DiagnosticSeverity.Warning, isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(ReturnTypeShouldBeTaskRule, GetOrDeleteCantHaveContentRule); } }

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
            AnalyzeReturnTypeShouldBeTask(context);
            AnalyzeGetCantHaveContent(context);
        }

        private void AnalyzeReturnTypeShouldBeTask(SymbolAnalysisContext context)
        {
            INamedTypeSymbol TaskOfT = context.Compilation.GetTypeByMetadataName("System.Threading.Tasks.Task`1");
            INamedTypeSymbol Task = context.Compilation.GetTypeByMetadataName("System.Threading.Tasks.Task");
            INamedTypeSymbol HttpAttribute = context.Compilation.GetTypeByMetadataName("WebAnchor.Attributes.URL.HttpAttribute");
            var methodSymbol = (IMethodSymbol)context.Symbol;
            if (methodSymbol.GetAttributes().Any(x => IsOrInheritsFrom(x.AttributeClass, HttpAttribute)))
            {
                var returnType = methodSymbol.ReturnType as ITypeSymbol;
                if (returnType != null && returnType.Kind != SymbolKind.ErrorType)
                {
                    if (!(IsOrInheritsFrom(returnType, Task) || IsOrInheritsFrom(returnType, TaskOfT)))
                    {
                        var node = methodSymbol.DeclaringSyntaxReferences[0].GetSyntax() as MethodDeclarationSyntax;
                        if (node != null)
                        {
                            var location = Location.Create(methodSymbol.DeclaringSyntaxReferences[0].SyntaxTree, node.ReturnType.Span);
                            var diagnostic = Diagnostic.Create(ReturnTypeShouldBeTaskRule, location, returnType.ToString());
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }

        private void AnalyzeGetCantHaveContent(SymbolAnalysisContext context)
        {
            INamedTypeSymbol TaskOfT = context.Compilation.GetTypeByMetadataName("System.Threading.Tasks.Task`1");
            INamedTypeSymbol Task = context.Compilation.GetTypeByMetadataName("System.Threading.Tasks.Task");
            INamedTypeSymbol GetAttribute = context.Compilation.GetTypeByMetadataName("WebAnchor.Attributes.URL.GetAttribute");
            INamedTypeSymbol DeleteAttribute = context.Compilation.GetTypeByMetadataName("WebAnchor.Attributes.URL.DeleteAttribute");
            INamedTypeSymbol ContentAttribute = context.Compilation.GetTypeByMetadataName("WebAnchor.Attributes.Content.ContentAttribute");
            var methodSymbol = (IMethodSymbol)context.Symbol;
            if (methodSymbol.GetAttributes().Any(x => IsOrInheritsFrom(x.AttributeClass, GetAttribute) || IsOrInheritsFrom(x.AttributeClass, DeleteAttribute)))
            {
                foreach (var offendingAttribute in methodSymbol.Parameters.SelectMany(x => x.GetAttributes().Where(a => IsOrInheritsFrom(a.AttributeClass, ContentAttribute))))
                {
                    var location = Location.Create(offendingAttribute.ApplicationSyntaxReference.SyntaxTree, offendingAttribute.ApplicationSyntaxReference.Span);
                    var diagnostic = Diagnostic.Create(GetOrDeleteCantHaveContentRule, location, 
                        methodSymbol.GetAttributes().First(x => IsOrInheritsFrom(x.AttributeClass, GetAttribute) || IsOrInheritsFrom(x.AttributeClass, DeleteAttribute)).AttributeClass.ToString());
                    context.ReportDiagnostic(diagnostic);
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
