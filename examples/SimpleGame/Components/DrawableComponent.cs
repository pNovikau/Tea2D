using SFML.Graphics;
using Tea2D.Ecs.Components;

namespace SimpleGame.Components;

public struct DrawableComponent : IComponent<DrawableComponent>
{
    public int Id { get; init; }

    public Drawable Drawable;
}