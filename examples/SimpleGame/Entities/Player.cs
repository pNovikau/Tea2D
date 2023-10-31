using System;
using SFML.Graphics;
using SFML.System;
using SimpleGame.Components;
using Tea2D.Common;
using Tea2D.Ecs;
using Tea2D.Graphics.Primitives;

namespace SimpleGame.Entities;

public static class Player
{
    public static void CreateRectangle(this GameWorldBase gameWorld)
    {
        var rnd = Random.Shared;

        var player = gameWorld.AddEntity();
        var rectangle = Rectangle.Create(new Vector2<float>(10, 10));

        ref var transformComponent = ref player.AddComponent<TransformComponent>();
        transformComponent.Transformable = rectangle;
        transformComponent.Transformable.Position = new Vector2<float>(600, 300);

        ref var drawableComponent = ref player.AddComponent<DrawableComponent>();
        drawableComponent.Drawable = rectangle;

        ref var moveComponent = ref player.AddComponent<MoveComponent>();
        moveComponent.Direction = new Vector2<float>(rnd.NextSingle(), rnd.NextSingle());
        moveComponent.Velocity = rnd.NextSingle();

        ref var lifetimeComponent = ref player.AddComponent<LifetimeComponent>();
        lifetimeComponent.LifetimeInMilliseconds = rnd.Next(500, 1_000);
    }
}