using SimpleGame.Components;
using Tea2D;
using Tea2D.Ecs.ComponentFilters;
using Tea2D.Graphics;

namespace SimpleGame.Systems;

public class DrawSystem : Tea2D.Ecs.Systems.System
{
    private ComponentsFilter<TransformComponent, DrawableComponent> _filter;

    public override void Initialize(GameContext context)
    {
        _filter = new ComponentsFilter<TransformComponent, DrawableComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
    }

    public override void Update(in GameContext context)
    {
        foreach (var (_, _, drawableComponentSpan) in _filter)
        {
            ref var drawableComponent = ref drawableComponentSpan[0];

            ApplicationProvider.Application.CurrentRenderWindow!.Draw(drawableComponent.Drawable);
        }
    }
}