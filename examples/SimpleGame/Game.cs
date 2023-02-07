using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SimpleGame.Entities;
using SimpleGame.Systems;
using Tea2D;
using Tea2D.Core;
using Tea2D.Core.Encoders;
using Tea2D.Core.Encoders.Text;
using Tea2D.Core.Memory;
using Tea2D.Ecs;

namespace SimpleGame;

public class Game
{
    private RenderWindow _window;
    private IGameWorld _gameWorld;

    public void Init()
    {
        _window = new RenderWindow(new VideoMode(1200, 600), "Tea2D");
        _window.SetFramerateLimit(60);
        _gameWorld = new GameWorld();

        var context = new GameContext
        {
            RenderWindow = _window,
            GameWorld = _gameWorld
        };

        _gameWorld.SystemManager.RegisterSystem<DrawSystem>(context);
        _gameWorld.SystemManager.RegisterSystem<MoveSystem>(context);

        _gameWorld.Initialize(context);

        for (var i = 0; i < 10000; i++)
        {
            _gameWorld.CreateRectangle();
        }
    }

    public void Run()
    {
        var context = new GameContext
        {
            RenderWindow = _window,
            GameWorld = _gameWorld,
            GameTime = new GameTime()
        };

        var title = new ValueString("Tea2D: 0", stackalloc char[255]);
        var fpsCounter = new FpsCounter(context.GameTime);

        while (_window.IsOpen)
        {
            _window.DispatchEvents();

            _window.Clear();
            title.Remove(7);

            foreach (var system in _gameWorld.SystemManager.Systems)
                system.Update(context);

            context.GameTime.Update();

            title.Append(fpsCounter.Fps);
            _window.SetTitle(ref title);
            
            _window.Display();
        }
    }
}

public static class SfmlApiExtensions
{
    //[SkipLocalsInit]
    public static void SetTitle(this Window window, ref ValueString title)
    {
        // Copy the title to a null-terminated UTF-32 byte array
        title.Append('\0');
        Span<byte> titleAsUtf32 = stackalloc byte[EncoderProvider.Utf32.GetByteCount(title)];
        EncoderProvider.Utf32.GetBytes(title, titleAsUtf32);

        unsafe
        {
            fixed (byte* titlePtr = titleAsUtf32)
            {
                sfRenderWindow_setUnicodeTitle(window.CPointer, (IntPtr)titlePtr);
            }
        }
    }

    //[SkipLocalsInit]
    public static void SetDisplayedText(this Text text, ref ValueString title)
    {
        // Copy the title to a null-terminated UTF-32 byte array
        title.Append('\0');
        Span<byte> titleAsUtf32 = stackalloc byte[EncoderProvider.Utf32.GetByteCount(title)];
        EncoderProvider.Utf32.GetBytes(title, titleAsUtf32);

        // Pass it to the C API
        unsafe
        {
            fixed (byte* ptr = titleAsUtf32)
            {
                sfText_setUnicodeString(text.CPointer, (IntPtr)ptr);
            }
        }
    }
    
    [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    static extern void sfText_setUnicodeString(IntPtr CPointer, IntPtr Text);
    
    [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfRenderWindow_setUnicodeTitle(IntPtr CPointer, IntPtr title);
}