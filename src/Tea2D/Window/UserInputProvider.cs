using Tea2D.Window.Sfml;

namespace Tea2D.Window;

public static class UserInputProvider
{
    public static readonly IMouse Mouse = new Mouse();
    public static readonly IKeyboard Keyboard = new Keyboard();
}