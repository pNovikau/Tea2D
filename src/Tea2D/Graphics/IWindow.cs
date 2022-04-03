using System;
using Silk.NET.SDL;
using Tea2D.Common;

namespace Tea2D.Graphics
{
    public interface IWindow : IDisposable
    {
        Vector2I CursorPosition { get; set; }
        bool Visible { get; set; }
        bool Focused { get; }
        string Title { get; set; }

        bool DispatchEvent(in Event @event);
    }
}