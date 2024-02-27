using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Tea2D.Core.Common;

[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay("[Vector2] X({X}) Y({Y})")]
public record struct Vector2<TNumber>(TNumber X, TNumber Y)
    where TNumber : INumber<TNumber>
{
    public static Vector2<TNumber> operator -(Vector2<TNumber> v) => new(-v.X, -v.Y);
    public static Vector2<TNumber> operator -(Vector2<TNumber> v1, Vector2<TNumber> v2) => new(v1.X - v2.X, v1.Y - v2.Y);
    public static Vector2<TNumber> operator +(Vector2<TNumber> v1, Vector2<TNumber> v2) => new(v1.X + v2.X, v1.Y + v2.Y);
    public static Vector2<TNumber> operator *(Vector2<TNumber> v, TNumber x) => new(v.X * x, v.Y * x);
    public static Vector2<TNumber> operator *(TNumber x, Vector2<TNumber> v) => new(v.X * x, v.Y * x);
    public static Vector2<TNumber> operator /(Vector2<TNumber> v, TNumber x) => new(v.X / x, v.Y / x);
}