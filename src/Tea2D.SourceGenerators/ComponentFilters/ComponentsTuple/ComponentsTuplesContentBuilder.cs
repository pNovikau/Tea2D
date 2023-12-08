using Tea2D.SourceGenerators.Utils;

namespace Tea2D.SourceGenerators.ComponentFilters.ComponentsTuple
{
    public sealed class ComponentsTuplesContentBuilder
    {
        private readonly SourceCodeBuilder _builder;

        public ComponentsTuplesContentBuilder()
        {
            _builder = new SourceCodeBuilder();
            _builder.AppendLine("// <auto-generated/>");
            _builder.AppendLine("namespace Tea2D.Ecs.ComponentFilters;");

            _builder.AppendLine();
        }

        public void Add(string[] genericParameters)
        {
            var genericParametersString = string.Join(", ", genericParameters);

            _builder.Append(
$$"""
[global::System.CodeDom.Compiler.GeneratedCode("{{ Constants.Name }}", "{{ Constants.Version }}")]
public ref struct ComponentsTuple<{{ genericParametersString }}>
    {{ "where {0} : struct, global::Tea2D.Ecs.Components.IComponent<{0}>".Format(genericParameters) }}
{
    public int EntityId;

    {{ "public global::CommunityToolkit.HighPerformance.Ref<{0}> Component{0};".Format(genericParameters) }}

    public ComponentsTuple(
        int entityId,
        {{ "global::CommunityToolkit.HighPerformance.Ref<{0}> component{0}".Format(genericParameters, ",") }}
        )
    {
        EntityId = entityId;

        {{ "Component{0} = component{0};".Format(genericParameters) }}
    }

    public void Deconstruct(
        out int entityId,
        {{ "out global::CommunityToolkit.HighPerformance.Ref<{0}> component{0}".Format(genericParameters, ",") }}
        )
    {
        entityId = EntityId;

        {{ "component{0} = Component{0};".Format(genericParameters) }}
    }
}
"""
            );

            _builder.AppendLine();
        }

        public string GetContent()
        {
            return _builder.ToString();
        }
    }
}