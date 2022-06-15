using System;
using System.Text;

namespace Tea2D.Core.Encoders.Text;

public class Utf32Encoder : IEncoder
{
    private static readonly Encoder Encoder = Encoding.UTF32.GetEncoder();
    
    public int GetByteCount(ReadOnlySpan<char> value) => Encoder.GetByteCount(value, true);

    public void GetBytes(ReadOnlySpan<char> value, Span<byte> bytes) => Encoder.GetBytes(value, bytes, true);
}