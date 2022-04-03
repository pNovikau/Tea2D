// See https://aka.ms/new-console-template for more information

using System;
using Silk.NET.SDL;
using Tea2D.Graphics;
using Window = Tea2D.Graphics.SDL.Window;

namespace SimpleGame
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello, World!");

            var window = new Window();
            window.Title = "Hello, World!";

            bool @break = false;

            window.KeyPressed += (IWindow sender, in KeyboardEvent @event) =>
            {
                Console.WriteLine("Key pressed: " + @event.Keysym.Scancode);

                if (@event.Keysym.Scancode == Scancode.ScancodeEscape)
                {
                    @break = true;
                }
            };

            window.KeyReleased += (IWindow sender, in KeyboardEvent @event) => Console.WriteLine("Key released: " + @event.Keysym.Scancode);

            window.ButtonPressed += (IWindow sender, in MouseButtonEvent @event) => Console.WriteLine("Button pressed: " + @event.Button + " X=" + @event.X + " Y=" + @event.Y);

            window.ButtonReleased += (IWindow sender, in MouseButtonEvent @event) => Console.WriteLine("Button released: " + @event.Button + " X=" + @event.X + " Y=" + @event.Y);

            while (@break == false)
            {
                Event @event = default;

                while (SdlProvider.SDL.Value.PollEvent(ref @event) > 0)
                {
                    window.DispatchEvent(@event);
                }
            }

            window.Dispose();
        }
    }
}