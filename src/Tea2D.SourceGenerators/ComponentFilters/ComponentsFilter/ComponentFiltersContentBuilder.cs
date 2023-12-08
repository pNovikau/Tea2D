﻿using Tea2D.SourceGenerators.Utils;

namespace Tea2D.SourceGenerators.ComponentFilters.ComponentsFilter
{
    public sealed class ComponentFiltersContentBuilder
    {
        private readonly SourceCodeBuilder _builder;

        public ComponentFiltersContentBuilder()
        {
            _builder = new SourceCodeBuilder();
            _builder.AppendLine("// <auto-generated/>");
            _builder.AppendLine("namespace Tea2D.Ecs.ComponentFilters;");
            _builder.AppendLine();
        }

        public void AppendFilter(params string[] templatesParams)
        {
            var templatesParamsString = string.Join(", ", templatesParams);

            _builder.Append(
$$"""
[global::System.CodeDom.Compiler.GeneratedCode("{{ Constants.Name }}", "{{ Constants.Version }}")]
public sealed partial class ComponentsFilter<{{ templatesParamsString }}>
{
    public ComponentsFilter(global::Tea2D.Ecs.GameWorldBase gameWorld) : base(gameWorld) { }

    public ComponentEnumerator GetEnumerator() => new(this);

    [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    protected override bool IsComponentTypeSupported(int componentType)
    {
        return
            {{ "global::Tea2D.Ecs.Components.IComponent<{0}>.ComponentType == componentType".Format(templatesParams, " ||") }}
            ;
    }

    [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    protected override bool IsEntitySupported(ref global::Tea2D.Ecs.Entity entity)
    {
        return
            {{ "entity.Components[global::Tea2D.Ecs.Components.IComponent<{0}>.ComponentType] != -1".Format(templatesParams, " &&") }}
            ;
    }

    public ref struct ComponentEnumerator
    {
        private readonly ComponentsFilter _componentsFilter;
        private readonly global::System.Span<int> _entitiesIds;
        private readonly global::System.Span<Entity> _entities;

        {{ "private readonly global::System.Span<{0}> _componentsSpan{0};".Format(templatesParams) }}

        private int _index = -1;

        public ComponentEnumerator(ComponentsFilter componentsFilter)
        {
            _componentsFilter = componentsFilter;
            _entities = componentsFilter.GameWorld.EntityManager.AsSpan();
            _entitiesIds = global::CommunityToolkit.HighPerformance.ListExtensions.AsSpan(componentsFilter.EntitiesIds);

            {{ "_componentsSpan{0} = componentsFilter.GameWorld.ComponentManager.GetComponentBucket<{0}>().AsSpan();".Format(templatesParams) }}

            componentsFilter.Freeze();
        }

        public global::Tea2D.Ecs.ComponentFilters.ComponentsTuple<{{ templatesParamsString }}> Current
        {
            get
            {
                ref var entity = ref _entities[_entitiesIds[_index]];

                {{ "var componentId{0} = entity.Components[global::Tea2D.Ecs.Components.IComponent<{0}>.ComponentType];".Format(templatesParams) }}

                {{ "ref var component{0} = ref _componentsSpan{0}[componentId{0}];".Format(templatesParams) }}

                return new global::Tea2D.Ecs.ComponentFilters.ComponentsTuple<{{ templatesParamsString }}>(
                    _entitiesIds[_index],
                    {{ "new global::CommunityToolkit.HighPerformance.Ref<{0}>(ref component{0})".Format(templatesParams, ",") }}
                );
            }
        }

        public bool MoveNext()
        {
            if (_index == _entitiesIds.Length - 1)
                return false;

            ++_index;
            return true;
        }

        public void Reset()
        {
            _index = -1;
            _componentsFilter.Unfreeze();
        }

        public void Dispose()
        {
            Reset();
        }
    }
}

"""
                );
        }

        public string GetContent()
        {
            return _builder.ToString();
        }
    }
}