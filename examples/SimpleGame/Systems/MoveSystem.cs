using SimpleGame.Components;
using Tea2D;
using Tea2D.Core.Common;
using Tea2D.Core.Diagnostics;
using Tea2D.Ecs.ComponentFilters;

namespace SimpleGame.Systems;

public class MoveSystem : Tea2D.Ecs.Systems.System
{
    private ComponentsFilter<MoveComponent, TransformComponent> _filter;

    public override void Initialize(GameContext context)
    {
        _filter = new ComponentsFilter<MoveComponent, TransformComponent>(context.GameWorld);
    }

    public override void Update(GameContext context)
    {
        using var _ = Metric.Execution.Record("MoveSystem.Update");
        
        foreach (var (_, moveComponentRef, transformComponentRef) in _filter)
        {
            ref var moveComponent = ref moveComponentRef.Value;
            ref var transformComponent = ref transformComponentRef.Value;

            transformComponent.Transformable.Position = new Vector2<float>(
                transformComponent.Transformable.Position.X + (moveComponent.Direction.X * moveComponent.Velocity),
                transformComponent.Transformable.Position.Y + ((moveComponent.Direction.Y * moveComponent.Velocity)));
        }
    }
}