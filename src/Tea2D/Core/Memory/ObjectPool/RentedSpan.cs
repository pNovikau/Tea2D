using System;
using System.Runtime.InteropServices;

namespace Tea2D.Core.Memory.ObjectPool
{
    [StructLayout(LayoutKind.Sequential)]
#pragma warning disable 660,661
    public readonly ref struct RentedSpan<T>
#pragma warning restore 660,661
    {
        public readonly Span<T> Span;

        internal readonly int StartIndex;
        internal readonly int EndIndex;

        public RentedSpan(Span<T> span, int startIndex, int endIndex)
        {
            Span = span;

            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        public static bool operator !=(RentedSpan<T> left, RentedSpan<T> right) => !(left == right);

        public static bool operator ==(RentedSpan<T> left, RentedSpan<T> right)
        {
            return left.Span == right.Span &&
                   left.EndIndex == right.EndIndex &&
                   left.StartIndex == right.StartIndex;
        }

        public void Dispose()
        {
            SimpleArrayPool<T>.Instance.Return(this);
        }
    }
}