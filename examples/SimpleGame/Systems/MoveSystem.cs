using SFML.Graphics;
using SFML.System;
using SimpleGame.Components;
using Tea2D;
using Tea2D.Ecs.ComponentFilters;

namespace SimpleGame.Systems;

public class MoveSystem : Tea2D.Ecs.Systems.System
{
    private ComponentsFilter<MoveComponent, TransformComponent> _filter;

    public override void Initialize(GameContext context)
    {
        _filter = new ComponentsFilter<MoveComponent, TransformComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
    }

    public override void Update(in GameContext context)
    {
        foreach (var (_, moveComponentSpan, transformComponentSpan) in _filter)
        {
            ref var moveComponent = ref moveComponentSpan[0];
            ref var transformComponent = ref transformComponentSpan[0];

            var offset = moveComponent.Velocity * moveComponent.Speed * context.GameTime.Delta.AsSeconds();
            var position = transformComponent.Transformable.Position + new Vector2f(offset.X, offset.Y);
            transformComponent.Transformable.Position = position;
        }
    }
}