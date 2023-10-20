using System;
using System.Diagnostics;

namespace Tea2D.Ecs.Components;

public struct ComponentBucket<TComponent> : IComponentBucket<TComponent>
    where TComponent : struct, IComponent
{
    private const int DefaultLength = 255;

    private TComponent[] _components;
    private int _componentIndex;

    private int[] _freeComponents;
    private int _freeComponentsIndex;

    public ComponentBucket()
    {
        _components = new TComponent[DefaultLength];
        _componentIndex = 0;

        _freeComponents = new int[DefaultLength];
        _freeComponentsIndex = 0;
    }

    public ref TComponent CreateComponent()
    {
        int id;

        if (_freeComponentsIndex != 0)
        {
            id = _freeComponents[_freeComponentsIndex--];

            _components[id] = new TComponent { Id = id };
            return ref _components[id];
        }

        if (_componentIndex + 1 >= _components.Length)
            Array.Resize(ref _components, _components.Length * 2);

        id = _componentIndex++;

        _components[id] = new TComponent { Id = id };

        return ref _components[id];
    }

    public ref TComponent GetComponent(int id)
    {
        Debug.Assert(id >= 0);

        return ref _components[id];
    }

    public void Delete(int id)
    {
        if (id == _componentIndex - 1)
        {
            --_componentIndex;

            return;
        }

        if (_freeComponentsIndex + 1 >= _freeComponents.Length)
            Array.Resize(ref _freeComponents, _freeComponents.Length * 2);

        _freeComponents[_freeComponentsIndex++] = id;
    }

    public Span<TComponent> AsSpan() => _components.AsSpan()[.._componentIndex];
    public Memory<TComponent> AsMemory() => _components.AsMemory()[.._componentIndex];
}