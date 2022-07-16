using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Tea2D.Common
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay("{ToString}")]
    public struct Vector2U : IEquatable<Vector2U>
    {
        public uint X;
        public uint Y;

        public Vector2U(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        public static Vector2U operator -(Vector2U v1, Vector2U v2) => new(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2U operator +(Vector2U v1, Vector2U v2) => new(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2U operator *(Vector2U v, uint x) => new(v.X * x, v.Y * x);
        public static Vector2U operator *(uint x, Vector2U v) => new(v.X * x, v.Y * x);
        public static Vector2U operator /(Vector2U v, uint x) => new(v.X / x, v.Y / x);

        public static bool operator ==(Vector2U v1, Vector2U v2) => v1.Equals(v2);
        public static bool operator !=(Vector2U v1, Vector2U v2) => !v1.Equals(v2);

        public override string ToString() => $"[{nameof(Vector2U)}] X({X}) Y({Y})";

        public bool Equals(Vector2U other) =>
            X == other.X &&
            Y == other.Y;

        public override bool Equals(object obj) =>
            obj is Vector2U other &&
            Equals(other);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}