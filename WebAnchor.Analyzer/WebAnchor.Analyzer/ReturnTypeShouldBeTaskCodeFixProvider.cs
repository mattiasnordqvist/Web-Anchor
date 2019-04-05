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

namespace WebAnchor.Analyzers
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ReturnTypeShouldBeTaskCodeFixProvider)), Shared]
    public class ReturnTypeShouldBeTaskCodeFixProvider : CodeFixProvider
    {
        private const string title = "Allow the internet to be asynchronous!";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(ReturnTypeShouldBeTaskAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

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
                var newRoot = root.ReplaceNode(returnType, SyntaxFactory.ParseTypeName($"Task<{methodDecl.ReturnType.ToString()}>").WithLeadingTrivia(returnType.GetLeadingTrivia()).WithTrailingTrivia(returnType.GetTrailingTrivia()));
                var newDocument = document.WithSyntaxRoot(newRoot);
                return newDocument;
            }
        }
    }
}
