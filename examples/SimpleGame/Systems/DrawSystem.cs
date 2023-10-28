using SimpleGame.Components;
using Tea2D;
using Tea2D.Core.Diagnostics;
using Tea2D.Ecs.ComponentFilters;

namespace SimpleGame.Systems;

public class DrawSystem : Tea2D.Ecs.Systems.System
{
    private ComponentsFilter<TransformComponent, DrawableComponent> _filter;

    public override void Initialize(GameContext context)
    {
        _filter = new ComponentsFilter<TransformComponent, DrawableComponent>(context.GameWorld);
    }

    public override void Update(GameContext context)
    {
        using var _ = Metrics.Execution.Record("DrawSystem.Update");

        foreach (var (_, _, drawableComponentRef) in _filter)
        {
            ref var drawableComponent = ref drawableComponentRef.Value;

            context.RenderWindow.Draw(drawableComponent.Drawable);
        }
    }
}