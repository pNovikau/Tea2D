using System;
using SFML.Graphics;
using SFML.System;
using SimpleGame.Components;
using Tea2D.Common;
using Tea2D.Ecs;

namespace SimpleGame.Entities;

public static class Player
{
    private static Font Font = new Font(@"C:\Work\sandbox\arialbd.ttf");
    
    public static void CreateRectangle(this IGameWorld gameWorld)
    {
        var rnd = new Random();
        
        var player = gameWorld.AddEntity();
        var shape = new RectangleShape(new Vector2f(10, 10));

        ref var transformComponent = ref player.AddComponent<TransformComponent>();
        transformComponent.Transformable = shape;
        transformComponent.Transformable.Position = new Vector2f(600, 300);

        ref var drawableComponent = ref player.AddComponent<DrawableComponent>();
        drawableComponent.Drawable = shape;

        ref var moveComponent = ref player.AddComponent<MoveComponent>();
        moveComponent.Direction = new Vector2F(rnd.NextSingle() * (1f - -1f) + -1f, rnd.NextSingle() * (1f - -1f) + -1f);
        moveComponent.Velocity = rnd.NextSingle();

#if DEBUG
        ref var debugComponent = ref player.AddComponent<DrawDebugInformationComponent>();
        
        debugComponent.Text = new Text(string.Empty, Font, 9);
        debugComponent.Text.FillColor = Color.Red;
#endif
    }
}