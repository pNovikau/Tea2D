using SFML.Graphics;

namespace Tea2D.Graphics;

public interface IRenderWindow : IWindow
{
    /// <summary>
    /// Clear the entire target with black color
    /// </summary>
    void Clear();

    /// <summary>
    /// Draw a drawable object
    /// </summary>
    void Draw(IDrawable drawable);

    /// <summary>
    /// Display the window on screen
    /// </summary>
    void Display();
}