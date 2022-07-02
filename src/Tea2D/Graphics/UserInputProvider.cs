using Tea2D.Graphics.Sfml;

namespace Tea2D.Graphics;

public static class UserInputProvider
{
    public static readonly IMouse Mouse = new Mouse();
    public static readonly IKeyboard Keyboard = new Keyboard();
}