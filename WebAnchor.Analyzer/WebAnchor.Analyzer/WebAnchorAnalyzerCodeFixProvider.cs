using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace WebAnchor.Analyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(WebAnchorAnalyzerCodeFixProvider)), Shared]
    public class WebAnchorAnalyzerCodeFixProvider : CodeFixProvider
    {
        private const string title = "Make task";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(WebAnchorAnalyzerAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the type declaration identified by the diagnostic.
            var t = root.FindToken(diagnosticSpan.Start);
            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<MethodDeclarationSyntax>().First();

           context.RegisterCodeFix(
               CodeAction.Create(
                   title,
                   c => MakeReturnTypeTask(context.Document, declaration, c),
                   equivalenceKey: title),
               diagnostic);
        }

        private async Task<Document> MakeReturnTypeTask(Document document, MethodDeclarationSyntax methodDecl, CancellationToken cancellationToken)
        {
            var returnType = methodDecl.ReturnType;
            var root = await document.GetSyntaxRootAsync();
            if (returnType.GetFirstToken().Kind() == SyntaxKind.VoidKeyword)
            {

                var newRoot = root.ReplaceNode(returnType, SyntaxFactory.ParseTypeName("Task").WithLeadingTrivia(returnType.GetLeadingTrivia()).WithTrailingTrivia(returnType.GetTrailingTrivia()));
                var newDocument = document.WithSyntaxRoot(newRoot);
                return newDocument;

            }
            else
            {
                var newRoot = root.ReplaceNode(returnType, SyntaxFactory.ParseTypeName($"Task<{methodDecl.ReturnType.GetFirstToken().Text}>").WithLeadingTrivia(returnType.GetLeadingTrivia()).WithTrailingTrivia(returnType.GetTrailingTrivia()));
                var newDocument = document.WithSyntaxRoot(newRoot);
                return newDocument;
            }
        }
    }
}
