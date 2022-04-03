using System;
using Silk.NET.SDL;
using Tea2D.Common;
using Tea2D.Core.Diagnostics.Logging;
using Tea2D.Core.Memory;
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

        public Vector2I Position
        {
            get => SdlApi.GetWindowPosition(ref _windowPointerHandler);
            set => SdlApi.SetWindowPosition(ref _windowPointerHandler, value);
        }
        
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

        public event WindowEventHandler<MouseButtonEvent> ButtonPressed;
        public event WindowEventHandler<MouseButtonEvent> ButtonReleased;
        public event WindowEventHandler<KeyboardEvent> KeyPressed;
        public event WindowEventHandler<KeyboardEvent> KeyReleased;

        public bool DispatchEvent(in Event @event)
        {
            switch ((EventType)@event.Type)
            {
                case EventType.Mousebuttondown:
                    ButtonPressed?.Invoke(this, in @event.Button);
                    break;

                case EventType.Mousebuttonup:
                    ButtonReleased?.Invoke(this, in @event.Button);
                    break;

                case EventType.Keydown:
                    KeyPressed?.Invoke(this, in @event.Key);
                    break;

                case EventType.Keyup:
                    KeyReleased?.Invoke(this, in @event.Key);
                    break;

                default:
                    return false;
            }

            return true;
        }

        #endregion

        #region IDisposable

        //TODO: handle finalize
        public void Dispose()
        {
            SdlApi.DestroyWindow(ref _windowPointerHandler);
            _windowPointerHandler.Dispose();

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}