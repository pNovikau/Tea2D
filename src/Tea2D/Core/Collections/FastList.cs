using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Tea2D.Core.Collections;

[DebuggerDisplay("Count = {" + nameof(Count) + "}")]
public class FastList<T> : IList<T>, IReadOnlyList<T>
{
    private const int DefaultCapacity = 4;

    private int _size;

    public int Count => _size;

    public bool IsReadOnly => false;

    public int Capacity
    {
        get => Items.Length;
        set
        {
            if (value == Items.Length)
                return;

            if (value > 0)
            {
                var destinationArray = new T[value];
                if (_size > 0)
                    Array.Copy(Items, 0, destinationArray, 0, _size);

                Items = destinationArray;
            }
            else
            {
                Items = Array.Empty<T>();
            }
        }
    }

    public T[] Items { get; private set; }

    public T this[int index]
    {
        get => Items[index];
        set => Items[index] = value;
    }

    public FastList()
    {
        Items = Array.Empty<T>();
    }

    public FastList(int capacity)
    {
        Debug.Assert(capacity >= 0, "capacity >= 0");

        Items = capacity == 0
            ? Array.Empty<T>()
            : new T[capacity];
    }

    public FastList(IEnumerable<T> enumerable)
    {
        Debug.Assert(enumerable != null, "collection != null");

        if (enumerable is ICollection<T> collection)
        {
            if (collection.Count == 0)
            {
                Items = Array.Empty<T>();
            }
            else
            {
                Items = new T[collection.Count];
                collection.CopyTo(Items, 0);
                _size = collection.Count;
            }
        }
        else
        {
            Items = new T[DefaultCapacity];

            using var enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext())
                Add(enumerator.Current);
        }
    }

    #region IList<T>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(T item)
    {
        if (_size == Items.Length)
            EnsureCapacity(_size + 1);

        Items[_size++] = item;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        //TODO: mb pass a flag to do not clear a ref type???
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            var size = _size;
            _size = 0;

            if (size > 0)
                Array.Clear(Items, 0, size);
        }
        else
        {
            _size = 0;
        }
    }

    public bool Contains(T item)
        => _size != 0 && IndexOf(item) != -1;

    public void CopyTo(T[] array, int arrayIndex)
        => Array.Copy(Items, 0, array, arrayIndex, _size);

    public bool Remove(T item)
    {
        var index = IndexOf(item);

        if (index >= 0)
        {
            RemoveAt(index);
            return true;
        }

        return false;
    }

    public void RemoveAt(int index)
    {
        Debug.Assert(index < _size);

        --_size;

        if (index < _size)
        {
            Array.Copy(Items, index + 1, Items, index, _size - index);
        }

        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            Items[_size] = default;
        }
    }

    public int IndexOf(T item)
    {
        return Array.IndexOf(Items, item, 0, _size);
    }

    public void Insert(int index, T item)
    {
        Debug.Assert(index >= _size, "index >= _size");

        if (_size == Items.Length)
            EnsureCapacity(_size + 1);

        if (index < _size)
            Array.Copy(Items, index, Items, index + 1, _size - index);

        Items[index] = item;
        ++_size;
    }

    private void EnsureCapacity(int min)
    {
        if (Items.Length >= min)
            return;

        var newCapacity = Items.Length == 0
            ? DefaultCapacity
            : Items.Length * 2;

        if (newCapacity < min)
            newCapacity = min;

        Capacity = newCapacity;
    }

    public Enumerator GetEnumerator() => new Enumerator(this);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => throw new NotImplementedException();
    IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

    #endregion

    #region Enumerator

    [StructLayout(LayoutKind.Sequential)]
    public struct Enumerator
    {
        private readonly FastList<T> _list;
        private int _index;
        private T _current;

        internal Enumerator(FastList<T> list)
        {
            _list = list;
            _index = 0;
            _current = default;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            var list = _list;

            if (_index < list._size)
            {
                _current = list.Items[_index];
                _index++;
                return true;
            }

            return MoveNextRare();
        }

        private bool MoveNextRare()
        {
            _index = _list._size + 1;
            _current = default;
            return false;
        }

        public T Current => _current;

        public void Reset()
        {
            _index = 0;
            _current = default;
        }
    }

    #endregion

}