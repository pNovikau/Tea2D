using SFML.Graphics;
using SFML.System;
using Tea2D.Core.Common;
using Tea2D.Core.Memory;

namespace Tea2D.Graphics.Primitives;

public class Rectangle : RectangleShape, ITransformable, IDrawable
{
    private bool _isDestroyed;

    public static Rectangle Create(Vector2<float> size = default)
    {
        var rectangle = ObjectPool<Rectangle>.Default.Get();
        rectangle._isDestroyed = false;
        rectangle.Size = new Vector2f(size.X, size.Y);

        return rectangle;
    }

    public void Destroy()
    {
        if (_isDestroyed)
            return;

        Size = default;
        Position = default;

        Origin = default;
        Position = default;
        Rotation = default;
        Scale = default;

        Texture = default;
        TextureRect = default;
        FillColor = default;
        OutlineColor = default;
        OutlineThickness = default;

        _isDestroyed = true;
        ObjectPool<Rectangle>.Default.Return(this);
    }

    public new Vector2<float> Position 
    { 
        get => new(base.Position.X, base.Position.Y);
        set => base.Position = new Vector2f(value.X, value.Y);
    }

    public new Vector2<float> Scale 
    { 
        get => new(base.Scale.X, base.Scale.Y);
        set => base.Scale = new Vector2f(value.X, value.Y);
    }

    public new Vector2<float> Origin 
    { 
        get => new(base.Origin.X, base.Origin.Y);
        set => base.Origin = new Vector2f(value.X, value.Y);
    }
}