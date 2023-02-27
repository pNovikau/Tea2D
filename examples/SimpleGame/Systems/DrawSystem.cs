using SFML.Graphics;
using SFML.System;
using SimpleGame.Components;
using Tea2D;
using Tea2D.Core.Memory;
using Tea2D.Ecs.ComponentFilters;

namespace SimpleGame.Systems;

public class DrawSystem : Tea2D.Ecs.Systems.System
{
    private ComponentsFilter<TransformComponent, DrawableComponent> _filter;

#if DEBUG
    private ComponentsFilter<DrawDebugInformationComponent, TransformComponent> _debugFilter;
#endif

    public override void Initialize(GameContext context)
    {
        _filter = new ComponentsFilter<TransformComponent, DrawableComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
        
#if DEBUG
        _debugFilter = new ComponentsFilter<DrawDebugInformationComponent, TransformComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
#endif
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, _, drawableComponentRef) in _filter)
        {
            ref var drawableComponent = ref drawableComponentRef.Value;

            context.RenderWindow.Draw(drawableComponent.Drawable);
        }

        var str = new ValueString(stackalloc char[100]);
        
#if DEBUG
        foreach (var (_, debugComponentRef, transformComponentRef) in _debugFilter)
        {
            ref var debugComponent = ref debugComponentRef.Value;
            ref var transformComponent = ref transformComponentRef.Value;

            debugComponent.Text.Position = transformComponent.Transformable.Position + new Vector2f(-15, -15);
            
            str.Append("x: ");
            str.Append((int)transformComponent.Transformable.Position.X);
            
            str.Append(" y: ");
            str.Append((int)transformComponent.Transformable.Position.Y);
            
            debugComponent.Text.SetDisplayedText(ref str);

            context.RenderWindow.Draw(debugComponent.Text);
            
            str.Clear();
        }
#endif
    }
}