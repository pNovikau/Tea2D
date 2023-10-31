using SFML.Graphics;
using Tea2D.Ecs.Components;
using Tea2D.Graphics.Primitives;

namespace SimpleGame.Components;

public struct TransformComponent : IComponent<TransformComponent>
{
    public int Id { get; init; }

    public ITransformable Transformable;

    public void Disable()
    {
        Transformable.Destroy();
    }
}