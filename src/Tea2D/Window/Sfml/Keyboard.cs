namespace Tea2D.Window.Sfml;

public class Keyboard : IKeyboard
{
    public bool IsKeyPressed(KeyboardKey key) => SfmlKeyboard.IsKeyPressed((SfmlKeyboard.Key)key);
}