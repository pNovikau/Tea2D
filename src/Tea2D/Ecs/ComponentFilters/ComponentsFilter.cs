using System;
using System.Diagnostics;
using Tea2D.Core.Collections;
using Tea2D.Ecs.Managers;
using Tea2D.Ecs.Managers.Events;

namespace Tea2D.Ecs.ComponentFilters;

public abstract class ComponentsFilter : IComponentFilter
{
    private readonly int[] _componentTypes;

    protected readonly FastList<int> EntitiesIds = new(255);
    protected readonly IEntityManager EntityManager;
    protected readonly IComponentManager ComponentManager;

    protected ComponentsFilter(IEntityManager entityManager, IComponentManager componentManager, params int[] componentTypes)
    {
        Debug.Assert(entityManager != null);
        Debug.Assert(componentManager != null);

        entityManager.Events.EntityAdded += OnEntityAdded;
        entityManager.Events.EntityRemoved += OnEntityRemoved;
        entityManager.Events.EntityComponentAdded += OnEntityComponentAdded;
        entityManager.Events.EntityComponentRemoved += OnEntityComponentRemoved;

        _componentTypes = componentTypes;
        EntityManager = entityManager;
        ComponentManager = componentManager;
    }

    protected virtual bool OnEntityAdded(ref EntityEventArgs args)
    {
        if (IsEntityContainsAllComponents(args.EntityId))
        {
            EntitiesIds.Add(args.EntityId);
            return true;
        }

        return false;
    }

    protected virtual bool OnEntityRemoved(ref EntityEventArgs args)
    {
        if (IsEntityContainsAllComponents(args.EntityId))
        {
            EntitiesIds.Remove(args.EntityId);
            return true;
        }

        return false;
    }

    protected virtual bool OnEntityComponentAdded(ref EntityComponentEventArgs args)
    {
        if (!IsEntityContainsAllComponents(args.EntityId))
            return false;

        EntitiesIds.Add(args.EntityId);
        return true;
    }

    protected virtual bool OnEntityComponentRemoved(ref EntityComponentEventArgs args)
    {
        if (Array.IndexOf(_componentTypes, args.ComponentType) == -1)
            return false;

        EntitiesIds.Remove(args.EntityId);
        return true;
    }

    private bool IsEntityContainsAllComponents(int entityId)
    {
        ref var entity = ref EntityManager.Get(entityId);
        var counter = 0;

        for (int i = 0; i < entity.ComponentsTypes.Count; i++)
        {
            if (Array.IndexOf(_componentTypes, entity.ComponentsTypes[i]) != -1)
                ++counter;
        }

        return counter == _componentTypes.Length;
    }

    public void Dispose()
    {
        EntityManager.Events.EntityAdded -= OnEntityAdded;
        EntityManager.Events.EntityRemoved -= OnEntityRemoved;
        EntityManager.Events.EntityComponentAdded -= OnEntityComponentAdded;
        EntityManager.Events.EntityComponentRemoved -= OnEntityComponentRemoved;
    }
}