using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;
using SFML.Window;
using Tea2D.Common;
using Tea2D.Core.Encoders.Text;
using Tea2D.Core.Memory;

namespace Tea2D.Graphics.Sfml.Native;

internal static unsafe partial class SfmlApi
{
    [SkipLocalsInit]
    public static SfmlRenderWindow CreateRenderWindow(Vector2U size, string title)
    {
        return new SfmlRenderWindow(new VideoMode(size.X, size.Y), title);
    }

    [SkipLocalsInit]
    public static void SetTitle(this SfmlRenderWindow window, ref ValueString title)
    {
        title.Append('\0');
        Span<byte> titleAsUtf32 = stackalloc byte[EncoderProvider.Utf32.GetByteCount(title)];
        EncoderProvider.Utf32.GetBytes(title, titleAsUtf32);

        fixed (byte* titlePtr = titleAsUtf32)
        {
            sfWindow_setUnicodeTitle(window.CPointer, (IntPtr)titlePtr);
        }
    }

    [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern IntPtr sfRenderWindow_createUnicode(VideoMode mode, IntPtr title, Styles style, ref ContextSettings @params);

    [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    static extern void sfRenderWindow_setUnicodeTitle(IntPtr cPointer, IntPtr title);
}