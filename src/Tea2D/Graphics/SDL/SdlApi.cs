using System;
using Silk.NET.SDL;
using Tea2D.Common;
using Tea2D.Core.Memory;
using SdlWindow = Silk.NET.SDL.Window;

namespace Tea2D.Graphics.SDL
{
    public static class SdlApi
    {
        private static readonly Sdl Sdl; 
        
        static SdlApi()
        {
            SdlProvider.InitFlags = Sdl.InitEverything;
            Sdl = SdlProvider.SDL.Value;
        }

        public static unsafe PointerHandler<SdlWindow> CreateWindow(WindowFlags flags)
        {
            var windowPointer = Sdl.CreateWindow(string.Empty, Sdl.WindowposUndefined, Sdl.WindowposUndefined, 640, 480, (uint) flags);

            return windowPointer != null
                ? new PointerHandler<SdlWindow>(windowPointer)
                : PointerHandler<SdlWindow>.Null;
        }

        public static Vector2I GetMouseState()
        {
            var x = 0;
            var y = 0;

            Sdl.GetMouseState(ref x, ref y);

            return new Vector2I(x, y);
        }

        public static void WarpMouseInWindow(ref PointerHandler<SdlWindow> pointerHandler, Vector2I vector)
        {
            Sdl.WarpMouseInWindow(pointerHandler, vector.X, vector.Y);
        }

        public static bool IsWindowVisible(ref PointerHandler<SdlWindow> pointerHandler)
        {
            return (Sdl.GetWindowFlags(pointerHandler) & (uint) WindowFlags.WindowShown) != 0;
        }

        public static void SetWindowVisibility(ref PointerHandler<SdlWindow> pointerHandler, bool isVisible)
        {
            if (isVisible)
                Sdl.ShowWindow(pointerHandler);
            else
                Sdl.HideWindow(pointerHandler);
        }

        public static bool IsWindowFocused(ref PointerHandler<SdlWindow> pointerHandler)
        {
            return (Sdl.GetWindowFlags(pointerHandler) & (uint) WindowFlags.WindowInputFocus) != 0;
        }

        public static unsafe string GetWindowTitle(ref PointerHandler<SdlWindow> pointerHandler)
        {
            return Sdl.GetWindowTitleS((SdlWindow*)pointerHandler);
        }

        public static void SetWindowTitle(ref PointerHandler<SdlWindow> pointerHandler, ReadOnlySpan<char> title)
        {
            //TODO: rework!!!
            var str = new string(title);
            Sdl.SetWindowTitle(pointerHandler, str);
        }

        public static void DestroyWindow(ref PointerHandler<SdlWindow> pointerHandler)
        {
            Sdl.DestroyWindow(pointerHandler);
        }
    }
}