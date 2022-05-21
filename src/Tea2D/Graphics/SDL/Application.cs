using System;
using System.Collections.Generic;
using System.Diagnostics;
using Silk.NET.SDL;
using Tea2D.Core.Memory;
using SdlWindow = Silk.NET.SDL.Window;

namespace Tea2D.Graphics.SDL
{
    public sealed class Application : IApplication
    {
        private readonly Dictionary<IntPtr, IWindow> _windows = new();

        public IWindow CurrentWindow { get; private set; }

        public IReadOnlyCollection<IWindow> Windows => _windows.Values;

        public void RegisterWindow<TWindow>(TWindow window)
            where TWindow : IWindow
        {
            Debug.Assert(window != null, "window != null");

            _windows.Add(window.PointerHandle, window);
        }

        public void UnregisterWindow<TWindow>(TWindow window)
            where TWindow : IWindow
        {
            Debug.Assert(window != null, "window != null");

            _windows.Remove(window.PointerHandle);
        }

        public void DispatchEvents()
        {
            Event @event = new();

            while (SdlApi.PollEvent(ref @event) > 0)
            {
                var windowPointerHandler = PointerHandler.Null<SdlWindow>();

                switch ((EventType)@event.Type)
                {
                    case EventType.Mousebuttondown:
                    case EventType.Mousebuttonup:
                        windowPointerHandler = SdlApi.GetWindowFromId(@event.Button.WindowID);
                        break;

                    case EventType.Keydown:
                    case EventType.Keyup:
                        windowPointerHandler = SdlApi.GetWindowFromId(@event.Key.WindowID);
                        break;

                    case EventType.Windowevent:
                        windowPointerHandler = SdlApi.GetWindowFromId(@event.Button.WindowID);

                        if ((WindowEventID)@event.Window.Event == WindowEventID.WindoweventFocusGained)
                            CurrentWindow = _windows[windowPointerHandler];
                        else if ((WindowEventID)@event.Window.Event == WindowEventID.WindoweventFocusLost)
                            CurrentWindow = null;
                        break;
                }

                if (windowPointerHandler == PointerHandler.Null<SdlWindow>())
                    continue;

                _windows[windowPointerHandler].DispatchEvent(@event);
            }
        }

        public void Dispose()
        {
            foreach (var (_, window) in _windows)
                window.Dispose();

            _windows.Clear();
            CurrentWindow = null;
        }
    }
}