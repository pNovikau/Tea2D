using SFML.Graphics;
using SFML.System;
using Tea2D.Common;

namespace Tea2D.Graphics.Sfml;

//TODO:
public class Rectangle : IRectangle
{
    private readonly RectangleShape _rectangle;

    private Vector2F _size;
    
    public Rectangle(Vector2F size)
    {
        _size = size;
        _rectangle = new RectangleShape(new Vector2f(size.X, size.Y));
    }

    public Rectangle(Vector2F size, Vector2F position)
    {
        _size = size;
        Transform.Position = position;

        _rectangle = new RectangleShape(new Vector2f(size.X, size.Y));
        _rectangle.Position = new Vector2f(position.X, position.Y);
    }

    public Vector2F Size
    {
        get => _size;
        set
        {
            _size = value;
            _rectangle.Size = new Vector2f(value.X, value.Y);
        }
    }

    public Transform Transform { get; set; } = new();
}