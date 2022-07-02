namespace Tea2D.Graphics.Sfml;

internal static class Extensions
{
    public static SfmlRenderWindow GetSfmlWindow(this IRenderWindow renderWindow)
    {
        return ((RenderWindow)renderWindow).GetSfmlRenderWindow();
    }
}