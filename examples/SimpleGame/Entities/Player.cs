using SFML.Graphics;
using SFML.System;
using SimpleGame.Components;
using Tea2D.Ecs;

namespace SimpleGame.Entities;

public static class Player
{
    public static void CreatePlayer(this IGameWorld gameWorld)
    {
        var player = gameWorld.AddEntity();
        var shape = new RectangleShape(new Vector2f(25, 25));

        ref var transformComponent = ref player.AddComponent<TransformComponent>();
        transformComponent.Transformable = shape;
        transformComponent.Transformable.Position = new Vector2f(600, 300);

        ref var drawableComponent = ref player.AddComponent<DrawableComponent>();
        drawableComponent.Drawable = shape;

        ref var moveComponent = ref player.AddComponent<MoveComponent>();
        moveComponent.Velocity = default;
        moveComponent.Speed = 100f;

        player.AddComponent<ControlComponent>();

        ref var gunComponent = ref player.AddComponent<GunComponent>();
        gunComponent.FireDelayInMilliseconds = 300;
    }
}