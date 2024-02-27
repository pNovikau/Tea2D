using System;
using SFML.Graphics;
using SimpleGame.Components;
using Tea2D.Core.Common;
using Tea2D.Ecs;
using Tea2D.Graphics.Primitives;

namespace SimpleGame.Entities;

public static class Player
{
    public static void CreateRectangle(this GameWorldBase gameWorld, Vector2<float> position)
    {
        var rnd = Random.Shared;

        var player = gameWorld.AddEntity();
        var rectangle = Rectangle.Create(new Vector2<float>(10, 10));
        rectangle.FillColor = new Color((byte)rnd.Next(byte.MaxValue), (byte)rnd.Next(byte.MaxValue), (byte)rnd.Next(byte.MaxValue));

        ref var transformComponent = ref player.AddComponent<TransformComponent>();
        transformComponent.Transformable = rectangle;
        transformComponent.Transformable.Position = position;

        ref var drawableComponent = ref player.AddComponent<DrawableComponent>();
        drawableComponent.Drawable = rectangle;

        ref var moveComponent = ref player.AddComponent<MoveComponent>();
        moveComponent.Direction = new Vector2<float>(rnd.NextSingle(), rnd.NextSingle());
        moveComponent.Velocity = 0.5f;

        ref var lifetimeComponent = ref player.AddComponent<LifetimeComponent>();
        lifetimeComponent.LifetimeInMilliseconds = rnd.Next(100, 300);
    }
}