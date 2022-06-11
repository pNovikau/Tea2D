using System;

namespace Tea2D.Core.Encoders.Text;

public interface IEncoder
{
    int GetByteCount(ReadOnlySpan<char> value);
    void GetBytes(ReadOnlySpan<char> value, Span<byte> bytes);
}