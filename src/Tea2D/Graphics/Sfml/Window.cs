using System;
using System.Diagnostics;
using SFML.System;
using Tea2D.Common;
using Tea2D.Core.Memory;
using Tea2D.Graphics.Sfml.Native;

namespace Tea2D.Graphics.Sfml;

[DebuggerDisplay("{ToString}")]
internal class Window : IWindow
{
    private readonly SfmlWindow _window;

    public Window(Vector2U size, ReadOnlySpan<char> title)
    {
        _window = SfmlApi.CreateWindow(size, title);
    }

    public Vector2I Position
    {
        get => new(_window.Position.X, _window.Position.Y);
        set => _window.Position = new Vector2i(value.X, value.Y);
    }

    public Vector2U Size
    {
        get => new(_window.Size.X, _window.Size.Y);
        set => _window.Size = new Vector2u(value.X, value.Y);
    }

    public void DispatchEvents() => _window.DispatchEvents();

    public void Dispose() => _window.Dispose();

    public void SetTitle(ref ValueString title) => _window.SetTitle(ref title);

    public override string ToString() => $"[{nameof(Window)}] Size({Size}) Position({Position})";
}