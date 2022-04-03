using System.Text;

namespace Tea2D.Vulkan.Utils;

internal static unsafe class StringConverter
{
    public static byte* ToPointer(ReadOnlySpan<char> value)
    {
        var byteCount = Encoding.UTF8.GetMaxByteCount(value.Length + 1);
        var pointer = stackalloc byte[byteCount];

        var bytesWritten = Encoding.UTF8.GetBytes(value, new Span<byte>(pointer, byteCount));
        pointer[bytesWritten] = 0;

        return pointer;
    }
}