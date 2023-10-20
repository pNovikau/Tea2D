using System;
using SFML.Graphics;
using SFML.System;
using SimpleGame.Components;
using Tea2D.Common;
using Tea2D.Ecs;

namespace SimpleGame.Entities;

public static class Player
{
    public static void CreateRectangle(this IGameWorld gameWorld)
    {
        var rnd = Random.Shared;

        var player = gameWorld.AddEntity();
        var shape = new RectangleShape(new Vector2f(10, 10));

        ref var transformComponent = ref player.AddComponent<TransformComponent>();
        transformComponent.Transformable = shape;
        transformComponent.Transformable.Position = new Vector2f(600, 300);

        ref var drawableComponent = ref player.AddComponent<DrawableComponent>();
        drawableComponent.Drawable = shape;

        ref var moveComponent = ref player.AddComponent<MoveComponent>();
        moveComponent.Direction = new Vector2<float>(rnd.NextSingle(), rnd.NextSingle());
        moveComponent.Velocity = rnd.NextSingle();

        ref var lifetimeComponent = ref player.AddComponent<LifetimeComponent>();
        lifetimeComponent.LifetimeInMilliseconds = rnd.Next(30, 60);
    }
}