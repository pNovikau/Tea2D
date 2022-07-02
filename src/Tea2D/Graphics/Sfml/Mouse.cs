using Tea2D.Common;

namespace Tea2D.Graphics.Sfml;

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