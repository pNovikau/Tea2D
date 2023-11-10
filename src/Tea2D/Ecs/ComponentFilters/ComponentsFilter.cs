using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tea2D.Ecs.Events;

namespace Tea2D.Ecs.ComponentFilters;

public abstract class ComponentsFilter : IComponentFilter
{
    //TODO: add metrics in order to track queues 
    private readonly Queue<int> _entitiesToAdd = new();
    private readonly Queue<int> _entitiesToRemove = new();

    protected internal readonly List<int> EntitiesIds = new(255);
    protected internal readonly GameWorldBase GameWorld;

    private bool IsFreeze { get; set; }

    protected internal ComponentsFilter(GameWorldBase gameWorld)
    {
        Debug.Assert(gameWorld != null);

        gameWorld.Events.EntityRemoved += OnEntityComponentRemoved;
        gameWorld.Events.EntityComponentAdded += OnEntityComponentAdded;
        gameWorld.Events.EntityComponentRemoved += OnEntityComponentRemoved;

        GameWorld = gameWorld;
    }

    internal void Freeze()
    {
        IsFreeze = true;
    }

    internal void Unfreeze()
    {
        IsFreeze = false;

        while (_entitiesToAdd.TryDequeue(out var entityId)) 
            EntitiesIds.Add(entityId);

        while (_entitiesToRemove.TryDequeue(out var entityId)) 
            EntitiesIds.Remove(entityId);
    }

    private bool OnEntityComponentAdded(ref EntityComponentEventArgs args)
    {
        if (!IsComponentTypeSupported(args.ComponentType))
            return false;

        ref var entity = ref GameWorld.EntityManager.Get(args.EntityId);
        if (!IsEntitySupported(ref entity))
            return false;

        AddEntity(args.EntityId);

        return true;
    }

    private bool OnEntityComponentRemoved(ref EntityComponentEventArgs args)
    {
        if (!IsComponentTypeSupported(args.ComponentType))
            return false;

        RemoveEntity(args.EntityId);

        return true;
    }

    private bool OnEntityComponentRemoved(ref EntityEventArgs args)
    {
        ref var entity = ref GameWorld.EntityManager.Get(args.EntityId);
        if (!IsEntitySupported(ref entity))
            return false;

        RemoveEntity(args.EntityId);

        return true;
    }

    private void AddEntity(int entityId)
    {
        if (IsFreeze)
            _entitiesToAdd.Enqueue(entityId);
        else
            EntitiesIds.Add(entityId);
    }

    private void RemoveEntity(int entityId)
    {
        if (IsFreeze)
            _entitiesToRemove.Enqueue(entityId);
        else
            EntitiesIds.Remove(entityId);
    }

    protected abstract bool IsComponentTypeSupported(int componentType);
    protected abstract bool IsEntitySupported(ref Entity entity);

    public void Dispose()
    {
        GameWorld.Events.EntityRemoved -= OnEntityComponentRemoved;
        GameWorld.Events.EntityComponentAdded -= OnEntityComponentAdded;
        GameWorld.Events.EntityComponentRemoved -= OnEntityComponentRemoved;

        GC.SuppressFinalize(this);
    }
}