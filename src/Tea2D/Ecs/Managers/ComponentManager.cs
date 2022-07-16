using System;
using System.Diagnostics;
using Tea2D.Ecs.Components;

namespace Tea2D.Ecs.Managers;

public class ComponentManager : IComponentManager
{
    private readonly IComponentBucket[] _components = new IComponentBucket[255];

    public ref TComponent CreateComponent<TComponent>()
        where TComponent : struct, IComponent<TComponent>
    {
        var componentBucket = GetComponentBucket<TComponent>();

        return ref componentBucket.GetComponent();
    }

    public ref TComponent GetComponent<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>
    {
        var componentBucket = GetComponentBucket<TComponent>();
        var componentBucketSpan = componentBucket.AsSpan();

        Debug.Assert(id < componentBucketSpan.Length);

        return ref componentBucketSpan[id];
    }

    public Span<TComponent> GetComponentAsSpan<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>
    {
        var componentBucket = GetComponentBucket<TComponent>();
        var componentBucketSpan = componentBucket.AsSpan();

        Debug.Assert(id < componentBucketSpan.Length);

        return componentBucketSpan[id..(id + 1)];
    }

    public void Delete<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>
    {
        var index = IComponent<TComponent>.ComponentType;

        Debug.Assert(_components[index] != null);

        var componentBucket = (IComponentBucket<TComponent>)_components[index];
        componentBucket.Delete(id);
    }

    public void Delete(int componentType, int id)
    {
        _components[componentType].Delete(id);
    }

    private IComponentBucket<TComponent> GetComponentBucket<TComponent>()
        where TComponent : struct, IComponent<TComponent>
    {
        var index = IComponent<TComponent>.ComponentType;

        _components[index] ??= new ComponentBucket<TComponent>();
        return (IComponentBucket<TComponent>)_components[index];
    }
}