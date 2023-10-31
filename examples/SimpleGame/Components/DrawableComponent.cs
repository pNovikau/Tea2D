using Tea2D.Ecs.Components;
using Tea2D.Graphics.Primitives;

namespace SimpleGame.Components;

public struct DrawableComponent : IComponent<DrawableComponent>
{
    public int Id { get; init; }

    public IDrawable Drawable;

    public void Disable()
    {
        Drawable.Destroy();
    }
}