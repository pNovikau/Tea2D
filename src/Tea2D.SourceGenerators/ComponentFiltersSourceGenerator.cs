using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Tea2D.SourceGenerators.ComponentFilters;
using Tea2D.SourceGenerators.ComponentFilters.ComponentsTuple;

namespace Tea2D.SourceGenerators
{
    [Generator]
    public sealed class ComponentFiltersSourceGenerator : IIncrementalGenerator
    {
        private const string GenerateComponentsFilterAttribute = "Tea2D.Ecs.ComponentFilters.GenerateComponentsFilterAttribute";

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var classDeclarations = context.SyntaxProvider.CreateSyntaxProvider(
                    predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                    transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
                .Where(static m => m is not null);

            IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)> compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

            context.RegisterSourceOutput(compilationAndClasses,
                static (spc, source) => Execute(source.Item1, source.Item2, spc));
        }

        private static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext context)
        {
            if (classes.IsDefaultOrEmpty)
                return;

            var componentsTuplesContentBuilder = new ComponentsTuplesContentBuilder();
            var componentFiltersContentBuilder = new ComponentFiltersContentBuilder();

            foreach (var classSymbol in classes.Distinct())
            {
                var constraintClauses = classSymbol.ConstraintClauses.Select(p => p.Name.ToString()).ToArray();

                componentsTuplesContentBuilder.Add(constraintClauses);
                componentFiltersContentBuilder.AppendFilter(constraintClauses);
            }

            context.AddSource("ComponentsTuples.g.cs", SourceText.From(componentsTuplesContentBuilder.GetContent(), Encoding.UTF8));
            context.AddSource("ComponentsFilters.g.cs", SourceText.From(componentFiltersContentBuilder.GetContent(), Encoding.UTF8));
        }

        private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        {
            return node is ClassDeclarationSyntax { AttributeLists: { Count: > 0 }, ConstraintClauses: { Count: > 0 } };
        }

        private static ClassDeclarationSyntax GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
        {
            var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

            foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
            {
                foreach (var attributeSyntax in attributeListSyntax.Attributes)
                {
                    if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                        continue;

                    var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                    var fullName = attributeContainingTypeSymbol.ToDisplayString();

                    if (fullName == GenerateComponentsFilterAttribute)
                    {
                        return classDeclarationSyntax;
                    }
                }
            }

            return null;
        }
    }
}