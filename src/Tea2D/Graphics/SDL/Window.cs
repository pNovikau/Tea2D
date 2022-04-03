using System;
using Silk.NET.SDL;
using Tea2D.Common;
using Tea2D.Core.Diagnostics.Logging;
using Tea2D.Core.Memory;
using Tea2D.Graphics.SDL.Events;
using SdlWindow = Silk.NET.SDL.Window;

namespace Tea2D.Graphics.SDL
{
    public class Window : IWindow
    {
        private static readonly ILogger Logger = Core.Diagnostics.Logging.Logger.Instance; 
        
        private PointerHandler<SdlWindow> _windowPointerHandler;

        static Window()
        {
            SdlProvider.InitFlags = Sdl.InitEverything;
        }

        public Window()
        {
            const WindowFlags flags = WindowFlags.WindowAllowHighdpi;

            _windowPointerHandler = SdlApi.CreateWindow(flags);

            if (_windowPointerHandler == PointerHandler<SdlWindow>.Null)
            {
                //TODO: log and throw exception
                Logger.LogFatal("Error");
            }
        }

        #region IWindow

        public Vector2I CursorPosition
        {
            get => SdlApi.GetMouseState();
            set => SdlApi.WarpMouseInWindow(ref _windowPointerHandler, value);
        }

        public bool Visible
        {
            get => SdlApi.IsWindowVisible(ref _windowPointerHandler);
            set => SdlApi.SetWindowVisibility(ref _windowPointerHandler, value);
        }

        public bool Focused => SdlApi.IsWindowFocused(ref _windowPointerHandler);

        public string Title
        {
            get => SdlApi.GetWindowTitle(ref _windowPointerHandler);
            set => SdlApi.SetWindowTitle(ref _windowPointerHandler, value);
        }


        #endregion
        
        public event EventHandler<MouseButtonPressEvent> ButtonPress;
        public event EventHandler<MouseButtonReleaseEvent> ButtonRelease;
        
        public bool DispatchEvent(in Event @event)
        {
            var eventDispatcher = new EventDispatcher(@event);

            return eventDispatcher.Dispatch<MouseButtonPressEvent>(OnMouseButtonPressed) ||
                   eventDispatcher.Dispatch<MouseButtonReleaseEvent>(OnMouseButtonReleased);
        }

        private void OnMouseButtonPressed(MouseButtonPressEvent obj) => ButtonPress?.Invoke(this, obj);
        private void OnMouseButtonReleased(MouseButtonReleaseEvent obj) => ButtonRelease?.Invoke(this, obj);

        #region IDisposable

        public void Dispose()
        {
            SdlApi.DestroyWindow(ref _windowPointerHandler);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}