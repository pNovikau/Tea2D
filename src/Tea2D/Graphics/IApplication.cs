using System;
using System.Collections.Generic;
using Tea2D.Common;

namespace Tea2D.Graphics
{
    public interface IApplication : IDisposable
    {
        IWindow? CurrentWindow { get; }
        IRenderWindow? CurrentRenderWindow { get; }
        IReadOnlyCollection<IWindow> Windows { get; }
        bool IsRunning { get; }

        IWindow RegisterWindow(Vector2U size, string title);
        IRenderWindow RegisterRenderWindow(Vector2U size, string title);
        void Stop();

        bool UnregisterWindow(IWindow window);

        void DispatchEvents();
    }
}