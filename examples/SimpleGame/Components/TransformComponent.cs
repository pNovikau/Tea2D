using SFML.Graphics;
using Tea2D.Ecs.Components;

namespace SimpleGame.Components;

public struct TransformComponent : IComponent<TransformComponent>
{
    public int Id { get; init; }

    public Transformable Transformable;
}