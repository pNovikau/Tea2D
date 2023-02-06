using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Tea2D.Common;

[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay("[Vector2F] X({X}) Y({Y})")]
public struct Vector2F : IEquatable<Vector2F>
{
    public float X;
    public float Y;

    public Vector2F(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static Vector2F operator -(Vector2F v) => new(-v.X, -v.Y);
    public static Vector2F operator -(Vector2F v1, Vector2F v2) => new(v1.X - v2.X, v1.Y - v2.Y);
    public static Vector2F operator +(Vector2F v1, Vector2F v2) => new(v1.X + v2.X, v1.Y + v2.Y);
    public static Vector2F operator *(Vector2F v, float x) => new(v.X * x, v.Y * x);
    public static Vector2F operator *(float x, Vector2F v) => new(v.X * x, v.Y * x);
    public static Vector2F operator /(Vector2F v, float x) => new(v.X / x, v.Y / x);

    public static bool operator ==(Vector2F v1, Vector2F v2) => v1.Equals(v2);
    public static bool operator !=(Vector2F v1, Vector2F v2) => !v1.Equals(v2);

    public override string ToString() => $"[Vector2F] X({X}) Y({Y})";

    public bool Equals(Vector2F other) =>
        X.Equals(other.X) &&
        Y.Equals(other.Y);

    public override bool Equals(object obj) =>
        obj is Vector2F other &&
        Equals(other);

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode() => HashCode.Combine(X, Y);
}