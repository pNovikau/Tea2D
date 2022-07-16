using System;
using System.Collections.Generic;
using SimpleGame.Components;
using SimpleGame.Entities;
using Tea2D;
using Tea2D.Common;
using Tea2D.Common.Math;
using Tea2D.Ecs.ComponentFilters;
using Tea2D.Graphics;
using Tea2D.Window;

namespace SimpleGame.Systems;

public sealed class ControlSystem : Tea2D.Ecs.Systems.System
{
    private ComponentsFilter<ControlComponent, MoveComponent> _controlFilter;
    private ComponentsFilter<GunComponent, TransformComponent> _shootFilter;

    public override void Initialize(GameContext context)
    {
        _controlFilter = new ComponentsFilter<ControlComponent, MoveComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
        _shootFilter = new ComponentsFilter<GunComponent, TransformComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
    }

    public override void Update(in GameContext context)
    {
        foreach (var (_, _, moveComponentSpan) in _controlFilter)
        {
            ref var moveComponent = ref moveComponentSpan[0];

            var direction = new Vector2F();

            if (UserInputProvider.Keyboard.IsKeyPressed(KeyboardKey.A))
                direction.X += -1;

            if (UserInputProvider.Keyboard.IsKeyPressed(KeyboardKey.D))
                direction.X += 1;

            if (UserInputProvider.Keyboard.IsKeyPressed(KeyboardKey.W))
                direction.Y += -1;

            if (UserInputProvider.Keyboard.IsKeyPressed(KeyboardKey.S))
                direction.Y += 1;

            moveComponent.Velocity = direction;
        }

        foreach (var (_, gunComponentSpan, transformComponentSpan) in _shootFilter)
        {
            if (UserInputProvider.Mouse.IsButtonPressed(MouseButton.Left))
            {
                ref var gunComponent = ref gunComponentSpan[0];

                if (context.GameTime.GetTime.AsMilliseconds() > gunComponent.LastFireTimeInMilliseconds + gunComponent.FireDelayInMilliseconds)
                {
                    gunComponent.LastFireTimeInMilliseconds = context.GameTime.GetTime.AsMilliseconds();
                    
                    ref var transformComponent = ref transformComponentSpan[0];
                    var playerPosition = new Vector2F(transformComponent.Transformable.Position.X, transformComponent.Transformable.Position.Y);
                    var mousePosition = UserInputProvider.Mouse.GetPosition(ApplicationProvider.Application.CurrentRenderWindow!);

                    var velocity = VectorMath.GetDirection(playerPosition, mousePosition);
                    context.GameWorld.CreateBullet(context.GameTime, new Vector2F(playerPosition.X, playerPosition.Y), velocity);
                }
            }
        }
    }
}

public sealed class DestroyEntitySystem : Tea2D.Ecs.Systems.System
{
    private readonly Queue<int> _destroyQueue = new(255);

    private ComponentsFilter<DestroyEntityComponent> _destroyEntityComponentFilter;

    public override void Initialize(GameContext context)
    {
        _destroyEntityComponentFilter = new ComponentsFilter<DestroyEntityComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
    }

    public override void Update(in GameContext context)
    {
        foreach (var (entityId, _) in _destroyEntityComponentFilter)
        {
            _destroyQueue.Enqueue(entityId);
        }

        while (_destroyQueue.TryDequeue(out var entityId))
        {
            context.GameWorld.DestroyEntity(entityId);
        }
    }
}

public sealed class LifetimeSystem : Tea2D.Ecs.Systems.System
{
    private ComponentsFilter<LifetimeComponent> _lifetimeComponentFilter;

    public override void Initialize(GameContext context)
    {
        _lifetimeComponentFilter = new ComponentsFilter<LifetimeComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
    }

    public override void Update(in GameContext context)
    {
        foreach (var (entityId, lifetimeComponentSpan) in _lifetimeComponentFilter)
        {
            ref var lifetimeComponent = ref lifetimeComponentSpan[0];

            if (lifetimeComponent.LifetimeInMilliseconds + lifetimeComponent.CreateAtInMilliseconds <= context.GameTime.GetTime.AsMilliseconds())
            {
                context.GameWorld.AddComponent<DestroyEntityComponent>(entityId);
            }
        }
    }
}