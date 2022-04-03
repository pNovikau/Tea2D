﻿using System;

namespace Tea2D.Core.Memory
{
    public readonly unsafe struct PointerHandler<T>
        where T : unmanaged
    {
        public static readonly PointerHandler<T> Null = new();

        private readonly T* _pointer;

        public PointerHandler(T* pointer)
        {
            _pointer = pointer;
        }

        public bool Equals(PointerHandler<T> other) => _pointer == other._pointer;

        public override bool Equals(object obj) => obj is PointerHandler<T> other && Equals(other);

        public override int GetHashCode() => HashCode.Combine((long)_pointer);

        public static bool operator ==(PointerHandler<T> left, PointerHandler<T> right) => left._pointer == right._pointer;
        public static bool operator !=(PointerHandler<T> left, PointerHandler<T> right) => !(left == right);

        public static implicit operator Span<T>(PointerHandler<T> pointerHandler) => new(pointerHandler._pointer, 0);
        public static explicit operator T*(PointerHandler<T> pointerHandler) => pointerHandler._pointer;
    }
}