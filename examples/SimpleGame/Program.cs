// See https://aka.ms/new-console-template for more information

using System;
using Silk.NET.SDL;
using Tea2D.Graphics;
using Tea2D.Graphics.SDL;
using Window = Tea2D.Graphics.SDL.Window;

namespace SimpleGame
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello, World!");

            var application = new Application();

            var window1 = new Window {Title = "window1"};
            window1.KeyPressed += KeyPressed;
            window1.KeyReleased += KeyReleased;
            window1.ButtonPressed += ButtonPressed;
            window1.ButtonReleased += ButtonReleased;
            window1.FocusGained += FocusGained;
            window1.FocusLost += FocusLost;

            var window2 = new Window {Title = "window2"};
            window2.KeyPressed += KeyPressed;
            window2.KeyReleased += KeyReleased;
            window2.ButtonPressed += ButtonPressed;
            window2.ButtonReleased += ButtonReleased;
            window2.FocusGained += FocusGained;
            window2.FocusLost += FocusLost;

            var window3 = new Window {Title = "window3"};
            window3.KeyPressed += KeyPressed;
            window3.KeyReleased += KeyReleased;
            window3.ButtonPressed += ButtonPressed;
            window3.ButtonReleased += ButtonReleased;
            window3.FocusGained += FocusGained;
            window3.FocusLost += FocusLost;

            application.RegisterWindow(window1);
            application.RegisterWindow(window2);
            application.RegisterWindow(window3);
            
            bool @break = false;

            while (@break == false)
            {
                application.DispatchEvents();
            }
            
            application.Dispose();
        }
        
        public static void FocusGained(IWindow sender, in WindowEvent @event)
        {
            Console.WriteLine($"[{sender.Title}] Focus gained.");
        }
        
        public static void FocusLost(IWindow sender, in WindowEvent @event)
        {
            Console.WriteLine($"[{sender.Title}] Focus lost.");
        }

        public static void KeyPressed(IWindow sender, in KeyboardEvent @event)
        {
            Console.WriteLine($"[{sender.Title}] Key pressed: " + @event.Keysym.Scancode);
        }

        public static void KeyReleased(IWindow sender, in KeyboardEvent @event)
        {
            Console.WriteLine($"[{sender.Title}] Key released: " + @event.Keysym.Scancode);
        }

        public static void ButtonPressed(IWindow sender, in MouseButtonEvent @event)
        {
            Console.WriteLine($"[{sender.Title}] Button pressed: " + @event.Button + " X=" + @event.X + " Y=" + @event.Y);
        }

        public static void ButtonReleased(IWindow sender, in MouseButtonEvent @event)
        {
            Console.WriteLine($"[{sender.Title}] Button released: " + @event.Button + " X=" + @event.X + " Y=" + @event.Y);
        }
    }
}