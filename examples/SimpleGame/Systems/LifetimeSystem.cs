using SFML.Window;
using SimpleGame.Components;
using SimpleGame.Entities;
using Tea2D;
using Tea2D.Core.Common;
using Tea2D.Core.Diagnostics;
using Tea2D.Ecs.ComponentFilters;

namespace SimpleGame.Systems;

public class LifetimeSystem : Tea2D.Ecs.Systems.System
{
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
                continue;
            }

            lifetimeComponent.LifetimeInMilliseconds = remainTimeInMilliseconds;
        }
    }
}

public class InputSystem : Tea2D.Ecs.Systems.System
{
    private ComponentsFilter<LifetimeComponent> _filter;

    public override void Initialize(GameContext context)
    {
        _filter = new ComponentsFilter<LifetimeComponent>(context.GameWorld);
    }

    public override void Update(GameContext context)
    {
        using var _ = Metric.Execution.Record("InputSystem.Update");

        if (Mouse.IsButtonPressed(Mouse.Button.Left))
        {
            var position = Mouse.GetPosition(context.RenderWindow);
            context.GameWorld.CreateRectangle(new Vector2<float>(position.X, position.Y));
        }
    }
}