using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Tea2D.Ecs.Components;
using Tea2D.Ecs.Managers;

//TODO: use source generator to generate filters and tuples

namespace Tea2D.Ecs.ComponentFilters;

public class ComponentsFilter<TComponent> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
{
    public ComponentsFilter(IEntityManager entityManager, IComponentManager componentManager)
        : base(entityManager, componentManager, IComponent<TComponent>.ComponentType) { }

    public Enumerator GetEnumerator() => new(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator
    {
        private readonly ComponentsFilter<TComponent> _componentFilter;

        private int _index = -1;

        public Enumerator(ComponentsFilter<TComponent> componentFilter) : this()
        {
            _componentFilter = componentFilter;
        }

        public bool MoveNext()
        {
            if (_index == _componentFilter.EntitiesIds.Count - 1)
                return false;

            _index++;
            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        public unsafe Span<TComponent> Current
        {
            get
            {
                ref var entity = ref _componentFilter.EntityManager.Get(_componentFilter.EntitiesIds[_index]);

                var componentId = entity.Components[IComponent<TComponent>.ComponentType];
                ref var component = ref _componentFilter.ComponentManager.GetComponent<TComponent>(componentId);

                return new Span<TComponent>(Unsafe.AsPointer(ref component), 1);
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() => Reset();
    }
}

public class ComponentsFilter<TComponent, TComponent1> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
{
    public ComponentsFilter(IEntityManager entityManager, IComponentManager componentManager)
        : base(entityManager, componentManager,
            IComponent<TComponent>.ComponentType,
            IComponent<TComponent1>.ComponentType) { }

    public Enumerator GetEnumerator() => new(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator
    {
        private readonly ComponentsFilter<TComponent, TComponent1> _componentFilter;

        private int _index = -1;

        public Enumerator(ComponentsFilter<TComponent, TComponent1> componentFilter) : this()
        {
            _componentFilter = componentFilter;
        }

        public bool MoveNext()
        {
            if (_index == _componentFilter.EntitiesIds.Count - 1)
                return false;

            _index++;
            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        public ComponentsTuple<TComponent, TComponent1> Current
        {
            get
            {
                ref var entity = ref _componentFilter.EntityManager.Get(_componentFilter.EntitiesIds[_index]);

                var componentId = entity.Components[IComponent<TComponent>.ComponentType];
                var component = _componentFilter.ComponentManager.GetComponentAsSpan<TComponent>(componentId);

                var componentId1 = entity.Components[IComponent<TComponent1>.ComponentType];
                var component1 = _componentFilter.ComponentManager.GetComponentAsSpan<TComponent1>(componentId1);

                return new ComponentsTuple<TComponent, TComponent1>(component, component1);
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() => Reset();
    }
}

public class ComponentsFilter<TComponent, TComponent1, TComponent2> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2>
{
    public ComponentsFilter(IEntityManager entityManager, IComponentManager componentManager)
        : base(entityManager, componentManager,
            IComponent<TComponent>.ComponentType,
            IComponent<TComponent1>.ComponentType,
            IComponent<TComponent2>.ComponentType) { }

    public Enumerator GetEnumerator() => new(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator
    {
        private readonly ComponentsFilter<TComponent, TComponent1, TComponent2> _componentFilter;

        private int _index = -1;

        public Enumerator(ComponentsFilter<TComponent, TComponent1, TComponent2> componentFilter) : this()
        {
            _componentFilter = componentFilter;
        }

        public bool MoveNext()
        {
            if (_index == _componentFilter.EntitiesIds.Count - 1)
                return false;

            _index++;
            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        public ComponentsTuple<TComponent, TComponent1, TComponent2> Current
        {
            get
            {
                ref var entity = ref _componentFilter.EntityManager.Get(_componentFilter.EntitiesIds[_index]);

                var componentId = entity.Components[IComponent<TComponent>.ComponentType];
                var component = _componentFilter.ComponentManager.GetComponentAsSpan<TComponent>(componentId);

                var componentId1 = entity.Components[IComponent<TComponent1>.ComponentType];
                var component1 = _componentFilter.ComponentManager.GetComponentAsSpan<TComponent1>(componentId1);

                var componentId2 = entity.Components[IComponent<TComponent2>.ComponentType];
                var component2 = _componentFilter.ComponentManager.GetComponentAsSpan<TComponent2>(componentId2);

                return new ComponentsTuple<TComponent, TComponent1, TComponent2>(component, component1, component2);
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() => Reset();
    }
}

public class ComponentsFilter<TComponent, TComponent1, TComponent2, TComponent3> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2>
    where TComponent3 : struct, IComponent<TComponent3>
{
    public ComponentsFilter(IEntityManager entityManager, IComponentManager componentManager)
        : base(entityManager, componentManager,
            IComponent<TComponent>.ComponentType,
            IComponent<TComponent1>.ComponentType,
            IComponent<TComponent2>.ComponentType,
            IComponent<TComponent3>.ComponentType) { }

    public Enumerator GetEnumerator() => new(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator
    {
        private readonly ComponentsFilter<TComponent, TComponent1, TComponent2, TComponent3> _componentFilter;

        private int _index = -1;

        public Enumerator(ComponentsFilter<TComponent, TComponent1, TComponent2, TComponent3> componentFilter) : this()
        {
            _componentFilter = componentFilter;
        }

        public bool MoveNext()
        {
            if (_index == _componentFilter.EntitiesIds.Count - 1)
                return false;

            _index++;
            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        public ComponentsTuple<TComponent, TComponent1, TComponent2, TComponent3> Current
        {
            get
            {
                ref var entity = ref _componentFilter.EntityManager.Get(_componentFilter.EntitiesIds[_index]);

                var componentId = entity.Components[IComponent<TComponent>.ComponentType];
                var component = _componentFilter.ComponentManager.GetComponentAsSpan<TComponent>(componentId);

                var componentId1 = entity.Components[IComponent<TComponent1>.ComponentType];
                var component1 = _componentFilter.ComponentManager.GetComponentAsSpan<TComponent1>(componentId1);

                var componentId2 = entity.Components[IComponent<TComponent2>.ComponentType];
                var component2 = _componentFilter.ComponentManager.GetComponentAsSpan<TComponent2>(componentId2);

                var componentId3 = entity.Components[IComponent<TComponent3>.ComponentType];
                var component3 = _componentFilter.ComponentManager.GetComponentAsSpan<TComponent3>(componentId3);
                
                return new ComponentsTuple<TComponent, TComponent1, TComponent2, TComponent3>(component, component1, component2, component3);
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() => Reset();
    }
}