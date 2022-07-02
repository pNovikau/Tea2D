namespace Tea2D.Graphics.Sfml;

public class Drawable : IDrawable
{
    private readonly SfmlDrawable _drawable;

    internal Drawable(SfmlDrawable drawable)
    {
        _drawable = drawable;
    }

    public void Draw(IRenderWindow renderWindow)
    {
        renderWindow.GetSfmlWindow().Draw(_drawable);
    }
}