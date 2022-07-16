using SFML.Graphics;
using SFML.System;
using SimpleGame.Components;
using Tea2D.Common;
using Tea2D.Core;
using Tea2D.Ecs;

namespace SimpleGame.Entities;

public static class Bullet
{
    public static void CreateBullet(this IGameWorld gameWorld, GameTime time, Vector2F position, Vector2F direction)
    {
        var bullet = gameWorld.AddEntity();

        var shape = new RectangleShape(new Vector2f(10, 10));
        shape.FillColor = Color.Red;

        ref var transformComponent = ref bullet.AddComponent<TransformComponent>();
        transformComponent.Transformable = shape;
        transformComponent.Transformable.Position = new Vector2f(position.X, position.Y);

        ref var drawableComponent = ref bullet.AddComponent<DrawableComponent>();
        drawableComponent.Drawable = shape;

        ref var moveComponent = ref bullet.AddComponent<MoveComponent>();
        moveComponent.Velocity = direction;
        moveComponent.Speed = 200f;

        ref var lifetimeComponent = ref bullet.AddComponent<LifetimeComponent>();
        lifetimeComponent.CreateAtInMilliseconds = time.GetTime.AsMilliseconds();
        lifetimeComponent.LifetimeInMilliseconds = 1000;
    }
}