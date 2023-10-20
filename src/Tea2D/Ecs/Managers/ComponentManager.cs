﻿using System.Diagnostics;
using CommunityToolkit.HighPerformance;
using Tea2D.Diagnostics;
using Tea2D.Ecs.Components;

namespace Tea2D.Ecs.Managers;

public class ComponentManager : IComponentManager
{
    private readonly IComponentBucket[] _components = new IComponentBucket[255];

    public ref TComponent CreateComponent<TComponent>()
        where TComponent : struct, IComponent<TComponent>
    {
        var componentBucket = GetComponentBucket<TComponent>();

        ref var component = ref componentBucket.CreateComponent();

        Metrics.Components<TComponent>.Increment();

        return ref component;
    }

    public ref TComponent GetComponent<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>
    {
        var componentBucket = GetComponentBucket<TComponent>();

        return ref componentBucket.GetComponent(id);
    }

    public Ref<TComponent> GetComponentAsRef<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>
    {
        var componentBucket = GetComponentBucket<TComponent>();
        ref var component = ref componentBucket.GetComponent(id);

        return new Ref<TComponent>(ref component);
    }

    public void Delete<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>
    {
        var index = IComponent<TComponent>.ComponentType;

        Debug.Assert(_components[index] != null);

        var componentBucket = (IComponentBucket<TComponent>)_components[index];
        componentBucket.Delete(id);

        Metrics.Components<TComponent>.Decrement();
    }

    public IComponentBucket<TComponent> GetComponentBucket<TComponent>()
        where TComponent : struct, IComponent<TComponent>
    {
        var index = IComponent<TComponent>.ComponentType;

        _components[index] ??= new ComponentBucket<TComponent>();
        return (IComponentBucket<TComponent>)_components[index];
    }
}