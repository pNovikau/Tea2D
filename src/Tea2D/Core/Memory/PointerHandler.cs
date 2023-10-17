using System;
using System.Runtime.CompilerServices;

namespace Tea2D.Core.Memory;

public static class PointerHandler
{
    public static PointerHandler<T> Null<T>() where T : unmanaged
        => new();
}

public unsafe struct PointerHandler<T> : IDisposable
    where T : unmanaged
{
    private T* _pointer;

    public PointerHandler(void* pointer)
    {
        _pointer = (T*)(pointer);
    }

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

    public static implicit operator IntPtr(PointerHandler<T> pointerHandler) => (IntPtr)pointerHandler._pointer;
    public static explicit operator T*(PointerHandler<T> pointerHandler) => pointerHandler._pointer;

    public ref T AsRef() => ref Unsafe.AsRef<T>(_pointer);

    public void Dispose() => _pointer = null;

    public override string ToString() => _pointer->ToString();
}