using System;
using Silk.NET.SDL;
using Tea2D.Common;
using Tea2D.Graphics;

public delegate void WindowEventHandler<TEvent>(IWindow sender, in TEvent @event) where TEvent : struct;

namespace Tea2D.Graphics
{
    public interface IWindow : IDisposable
    {
        IntPtr PointerHandle { get; }

        Vector2I Position { get; set; }
        Vector2I CursorPosition { get; set; }
        bool Visible { get; set; }
        bool IsFocused { get; }
        string Title { get; set; }

        bool DispatchEvent(in Event @event);

        event WindowEventHandler<MouseButtonEvent> ButtonPressed;
        event WindowEventHandler<MouseButtonEvent> ButtonReleased;
        event WindowEventHandler<KeyboardEvent> KeyPressed;
        event WindowEventHandler<KeyboardEvent> KeyReleased;
    }
}