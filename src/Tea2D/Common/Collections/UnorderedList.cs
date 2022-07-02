using System;
using Tea2D.Ecs;

namespace Tea2D.Common.Collections;

public class UnorderedList<TItem> : IUnorderedList<TItem>
    where TItem : struct, IHasId
{
    private const int DefaultCapacity = 255;

    private TItem[] _array;
    private int _arrayIndex;

    private int[] _reusableSlots;
    private int _reusableSlotsIndex;

    public UnorderedList() : this(DefaultCapacity) { }

    public UnorderedList(int capacity)
    {
        _array = new TItem[capacity];
        _arrayIndex = 0;

        _reusableSlots = new int[capacity];
        _reusableSlotsIndex = 0;
    }

    public TItem[] Items => _array;

    public ref TItem Get()
    {
        int id;

        if (_reusableSlotsIndex != 0)
        {
            id = _reusableSlots[--_reusableSlotsIndex];
            _array[id] = new TItem { Id = id };

            return ref _array[id];
        }

        if (_arrayIndex == _array.Length)
            Array.Resize(ref _array, _array.Length * 2);

        id = _arrayIndex++;
        _array[id] = new TItem { Id = id };

        return ref _array[id];
    }

    public void Remove(int id)
    {
        if (_reusableSlotsIndex == _reusableSlots.Length)
            Array.Resize(ref _reusableSlots, _reusableSlots.Length * 2);

        _reusableSlots[_reusableSlotsIndex++] = id;
    }

    public void Clear()
    {
        _arrayIndex = 0;
        _reusableSlotsIndex = 0;
    }
}