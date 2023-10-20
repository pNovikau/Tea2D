using System.Collections.Generic;
using SimpleGame.Components;
using SimpleGame.Entities;
using Tea2D;
using Tea2D.Diagnostics;
using Tea2D.Ecs.ComponentFilters;

namespace SimpleGame.Systems;

public class LifetimeSystem : Tea2D.Ecs.Systems.System
{
    private const int MaxEntities = 2000;
    
    private readonly List<int> _entitiesToRemove = new();
    private int _totalEntities = 0;

    private ComponentsFilter<LifetimeComponent> _filter;

    public override void Initialize(GameContext context)
    {
        _filter = new ComponentsFilter<LifetimeComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
    }

    public override void Update(GameContext context)
    {
        using var _ = Metrics.Execution.Record("LifetimeSystem.Update");
        
        foreach (var (entityId, lifetimeComponentRef) in _filter)
        {
            ref var lifetimeComponent = ref lifetimeComponentRef.Value;

            var delta = context.GameTime.Delta.AsMilliseconds();
            var remainTimeInMilliseconds = lifetimeComponent.LifetimeInMilliseconds - delta;

            if (remainTimeInMilliseconds <= 0)
            {
                _entitiesToRemove.Add(entityId);
                continue;
            }

            lifetimeComponent.LifetimeInMilliseconds = remainTimeInMilliseconds;
        }

        // TODO: revisit delete components/entities logic
        // TODO: implement queue to delete/add components/entities 
        foreach (var id in _entitiesToRemove)
        {
            --_totalEntities;
            context.GameWorld.EntityManager.Remove(id);
        }

        _entitiesToRemove.Clear();

        for (; _totalEntities < MaxEntities; _totalEntities++)
        {
            context.GameWorld.CreateRectangle();
        }
    }
}