using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Tea2D.Common
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay("[Vector2I] X({X}) Y({Y})")]
    public struct Vector2I : IEquatable<Vector2I>
    {
        public int X;
        public int Y;

        public Vector2I(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2I operator -(Vector2I v) => new(-v.X, -v.Y);
        public static Vector2I operator -(Vector2I v1, Vector2I v2) => new(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2I operator +(Vector2I v1, Vector2I v2) => new(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2I operator *(Vector2I v, int x) => new(v.X * x, v.Y * x);
        public static Vector2I operator *(int x, Vector2I v) => new(v.X * x, v.Y * x);
        public static Vector2I operator /(Vector2I v, int x) => new(v.X / x, v.Y / x);

        public static bool operator ==(Vector2I v1, Vector2I v2) => v1.Equals(v2);
        public static bool operator !=(Vector2I v1, Vector2I v2) => !v1.Equals(v2);

        public override string ToString() => $"[Vector2F] X({X}) Y({Y})";

        public bool Equals(Vector2I other) =>
            X.Equals(other.X) &&
            Y.Equals(other.Y);

        public override bool Equals(object obj) =>
            obj is Vector2I other &&
            Equals(other);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}