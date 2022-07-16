using System;
using System.Diagnostics;
using SFML.Graphics;
using SFML.System;
using Tea2D.Common;
using Tea2D.Core.Memory;
using Tea2D.Graphics.Sfml.Native;

namespace Tea2D.Graphics.Sfml;

[DebuggerDisplay("{ToString}")]
internal class RenderWindow : IRenderWindow
{
    private readonly SfmlRenderWindow _renderWindow;

    public RenderWindow(Vector2U size, string title)
    {
        _renderWindow = SfmlApi.CreateRenderWindow(size, title);
        _renderWindow.SetFramerateLimit(60);
    }

    public Vector2I Position
    {
        get => new(_renderWindow.Position.X, _renderWindow.Position.Y);
        set => _renderWindow.Position = new Vector2i(value.X, value.Y);
    }

    public Vector2U Size
    {
        get => new(_renderWindow.Size.X, _renderWindow.Size.Y);
        set => _renderWindow.Size = new Vector2u(value.X, value.Y);
    }

    public void Clear() => _renderWindow.Clear();

    public void Draw(IDrawable drawable)
    {
        Debug.Assert(drawable != null);

        drawable.Draw(this);
    }

    public void Display() => _renderWindow.Display();

    public void DispatchEvents() => _renderWindow.DispatchEvents();

    public void Dispose() => _renderWindow.Dispose();

    public void SetTitle(ref ValueString title) => _renderWindow.SetTitle(ref title);

    public override string ToString() => $"[{nameof(RenderWindow)}] Size({Size}) Position({Position})";

    internal SfmlRenderWindow GetSfmlRenderWindow() => _renderWindow;
}