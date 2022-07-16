using System.Collections.Generic;
using System.Diagnostics;
using Tea2D.Common;
using Tea2D.Graphics.Sfml;

namespace Tea2D.Graphics;

internal class Application : IApplication
{
    private readonly List<IWindow> _windows = new();

    public IWindow? CurrentWindow { get; private set; }
    public IRenderWindow? CurrentRenderWindow { get; private set; }
    public IReadOnlyCollection<IWindow> Windows => _windows;
    public bool IsRunning { get; private set; } = true;

    public IWindow RegisterWindow(Vector2U size, string title)
    {
        Debug.Assert(IsRunning);

        var window = new Sfml.Window(size, title);
        CurrentWindow ??= window;

        _windows.Add(window);

        return window;
    }

    public IRenderWindow RegisterRenderWindow(Vector2U size, string title)
    {
        Debug.Assert(IsRunning);
        Debug.Assert(CurrentRenderWindow == null);

        var renderWindow = new RenderWindow(size, title);
        CurrentWindow ??= renderWindow;
        CurrentRenderWindow ??= renderWindow;

        _windows.Add(renderWindow);

        return renderWindow;
    }

    public void Stop()
    {
        Debug.Assert(IsRunning);

        IsRunning = false;
    }

    public bool UnregisterWindow(IWindow window)
    {
        Debug.Assert(IsRunning);
        Debug.Assert(window != null);
        Debug.Assert(_windows.Contains(window));

        return _windows.Remove(window);
    }

    public void DispatchEvents()
    {
        Debug.Assert(IsRunning);

        foreach (var window in _windows)
            window.DispatchEvents();
    }

    public void Dispose()
    {
        Stop();

        foreach (var window in _windows)
            window.Dispose();

        _windows.Clear();
    }
}