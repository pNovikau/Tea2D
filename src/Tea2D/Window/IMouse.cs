using Tea2D.Common;
using Tea2D.Graphics;

namespace Tea2D.Window;

public interface IMouse
{
    Vector2F GetPosition();
    Vector2F GetPosition(IRenderWindow renderWindow);

    bool IsButtonPressed(MouseButton button);
}

public enum MouseButton
{
    /// <summary>The left mouse button</summary>
    Left,

    /// <summary>The right mouse button</summary>
    Right,

    /// <summary>The middle (wheel) mouse button</summary>
    Middle,

    /// <summary>The first extra mouse button</summary>
    XButton1,

    /// <summary>The second extra mouse button</summary>
    XButton2,

    /// <summary>Keep last -- the total number of mouse buttons</summary>
    ButtonCount
};