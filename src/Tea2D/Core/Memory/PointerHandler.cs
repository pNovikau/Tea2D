using System;

namespace Tea2D.Core.Memory
{
    public unsafe struct PointerHandler<T> : IDisposable
        where T : unmanaged
    {
        public static readonly PointerHandler<T> Null = new();

        private T* _pointer;

        public PointerHandler(T* pointer)
        {
            _pointer = pointer;
        }

        public bool Equals(PointerHandler<T> other) => _pointer == other._pointer;

        public override bool Equals(object obj) => obj is PointerHandler<T> other && Equals(other);

        // ReSharper disable once NonReadonlyMemberInGetHashCode
        public override int GetHashCode() => HashCode.Combine((long)_pointer);

        public static bool operator ==(PointerHandler<T> left, PointerHandler<T> right) => left._pointer == right._pointer;
        public static bool operator !=(PointerHandler<T> left, PointerHandler<T> right) => !(left == right);

        public static explicit operator T*(PointerHandler<T> pointerHandler) => pointerHandler._pointer;

        public void Dispose() => _pointer = null;
    }
}