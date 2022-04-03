using System;
using Silk.NET.SDL;
using Tea2D.Common;

public delegate void WindowEventHandler<TEvent>(object sender, in TEvent @event) where TEvent : struct;

namespace Tea2D.Graphics
{
    public interface IWindow : IDisposable
    {
        Vector2I CursorPosition { get; set; }
        bool Visible { get; set; }
        bool Focused { get; }
        string Title { get; set; }

        bool DispatchEvent(in Event @event);

        event WindowEventHandler<MouseButtonEvent> ButtonPressed;
        event WindowEventHandler<MouseButtonEvent> ButtonReleased;
        event WindowEventHandler<KeyboardEvent> KeyPressed;
        event WindowEventHandler<KeyboardEvent> KeyReleased;
    }
}