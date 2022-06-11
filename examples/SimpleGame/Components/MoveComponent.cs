using Tea2D.Common;
using Tea2D.Ecs.Components;

namespace SimpleGame.Components;

public struct MoveComponent : IComponent<MoveComponent>
{
    public int Id { get; init; }

    public Vector2F Direction;
    public float Velocity;
}