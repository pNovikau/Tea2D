using System;
using System.Diagnostics;
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
    public static SfmlWindow CreateWindow(Vector2U size, ReadOnlySpan<char> title)
    {
        Span<char> titleWithNullTerminator = stackalloc char[title.Length + 1];
        title.CopyTo(titleWithNullTerminator);
        titleWithNullTerminator[title.Length - 1] = '\0';

        var byteCount = EncoderProvider.Utf32.GetByteCount(titleWithNullTerminator);
        Span<byte> bytes = stackalloc byte[byteCount];

        EncoderProvider.Utf32.GetBytes(titleWithNullTerminator, bytes);

        return CreateWindow(size, bytes);
    }

    [SkipLocalsInit]
    public static SfmlWindow CreateWindow(Vector2U size, ref ValueString title)
    {
        title.Append('\0');

        var byteCount = EncoderProvider.Utf32.GetByteCount(title);
        Span<byte> bytes = stackalloc byte[byteCount];

        EncoderProvider.Utf32.GetBytes(title, bytes);

        return CreateWindow(size, bytes);
    }

    [SkipLocalsInit]
    public static void SetTitle(this SfmlWindow window, ref ValueString title)
    {
        title.Append('\0');
        Span<byte> titleAsUtf32 = stackalloc byte[EncoderProvider.Utf32.GetByteCount(title)];
        EncoderProvider.Utf32.GetBytes(title, titleAsUtf32);

        fixed (byte* titlePtr = titleAsUtf32)
        {
            sfWindow_setUnicodeTitle(window.CPointer, (IntPtr)titlePtr);
        }
    }

    private static SfmlWindow CreateWindow(Vector2U size, Span<byte> title)
    {
        fixed (byte* titlePtr = title)
        {
            var settings = new ContextSettings(0, 0);
            var windowPointer = sfWindow_createUnicode(new VideoMode(size.X, size.Y), (IntPtr)titlePtr, Styles.Default, ref settings);

            return new SfmlWindow(windowPointer);
        }
    }

    [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern IntPtr sfWindow_createUnicode(VideoMode mode, IntPtr title, Styles style, ref ContextSettings @params);

    [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfWindow_setUnicodeTitle(IntPtr cPointer, IntPtr title);
}