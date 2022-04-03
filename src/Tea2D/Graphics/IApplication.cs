using System;
using System.Collections.Generic;

namespace Tea2D.Graphics
{
    public interface IApplication : IDisposable
    {
        IWindow CurrentWindow { get; }
        IReadOnlyCollection<IWindow> Windows { get; }

        void RegisterWindow<TWindow>(TWindow window) where TWindow : IWindow;
        void UnregisterWindow<TWindow>(TWindow window) where TWindow : IWindow;

        void DispatchEvents();
    }
}