using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Tea2D.Core.Memory.Pools;

[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay("StartIndex: {StartIndex} Length: {Length}")]
public readonly ref struct RentedSpan<T>
{
    public readonly Span<T> Span;

    internal readonly int StartIndex;
    internal readonly int Length;

    public RentedSpan(Span<T> span, int startIndex, int length)
    {
        Span = span;

        StartIndex = startIndex;
        Length = length;
    }

    public static bool operator !=(RentedSpan<T> left, RentedSpan<T> right) => !(left == right);

    public static bool operator ==(RentedSpan<T> left, RentedSpan<T> right)
    {
        return left.Span == right.Span &&
               left.Length == right.Length &&
               left.StartIndex == right.StartIndex;
    }

    public void Dispose()
    {
        ObjectPool<T>.Instance.Return(in this);
    }

    public override bool Equals(object obj)
    {
        throw new NotImplementedException();
    }
}