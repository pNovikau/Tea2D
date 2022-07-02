namespace Tea2D.Graphics.Sfml;

public class Keyboard : IKeyboard
{
    public bool IsKeyPressed(KeyboardKey key) => SfmlKeyboard.IsKeyPressed((SfmlKeyboard.Key)key);
}