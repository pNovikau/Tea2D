using SimpleGame.Components;
using SimpleGame.Entities;
using Tea2D;
using Tea2D.Core.Diagnostics;
using Tea2D.Ecs.ComponentFilters;

namespace SimpleGame.Systems;

public class LifetimeSystem : Tea2D.Ecs.Systems.System
{
    private const int MaxEntities = 1000;
    private int _totalEntities = 0;

    private ComponentsFilter<LifetimeComponent> _filter;

    public override void Initialize(GameContext context)
    {
        _filter = new ComponentsFilter<LifetimeComponent>(context.GameWorld);
    }

    public override void Update(GameContext context)
    {
        using var _ = Metric.Execution.Record("LifetimeSystem.Update");
        
        foreach (var (entityId, lifetimeComponentRef) in _filter)
        {
            ref var lifetimeComponent = ref lifetimeComponentRef.Value;

            var delta = context.GameTime.Delta.AsMilliseconds();
            var remainTimeInMilliseconds = lifetimeComponent.LifetimeInMilliseconds - delta;

            if (remainTimeInMilliseconds <= 0)
            {
                context.GameWorld.DestroyEntity(entityId);
                --_totalEntities;
                continue;
            }

            lifetimeComponent.LifetimeInMilliseconds = remainTimeInMilliseconds;
        }

        for (var i = _totalEntities; i < MaxEntities; i++)
        {
            context.GameWorld.CreateRectangle();
            ++_totalEntities;
        }
    }
}