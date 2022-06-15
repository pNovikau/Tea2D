namespace Tea2D.Graphics;

public interface IRenderWindow : IWindow
{
    /// <summary>
    /// Clear the entire target with black color
    /// </summary>
    void Clear();

    //TODO:
    void Draw();

    /// <summary>
    /// Display the window on screen
    /// </summary>
    void Display();
}