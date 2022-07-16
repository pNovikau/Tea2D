using Tea2D.Common;
using Tea2D.Graphics;
using Tea2D.Graphics.Sfml;

namespace Tea2D.Window.Sfml;

public class Mouse : IMouse
{
    public Vector2F GetPosition()
    {
        return new Vector2F(
            SfmlMouse.GetPosition().X,
            SfmlMouse.GetPosition().Y
        );
    }

    public Vector2F GetPosition(IRenderWindow renderWindow)
    {
        return new Vector2F(
            SfmlMouse.GetPosition(renderWindow.GetSfmlWindow()).X,
            SfmlMouse.GetPosition(renderWindow.GetSfmlWindow()).Y
        );
    }

    public bool IsButtonPressed(MouseButton button)
    {
        return SfmlMouse.IsButtonPressed((SfmlMouse.Button)button);
    }
}