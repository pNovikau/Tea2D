using System;

namespace Tea2D.Core.Collections;

public class ReusableList<TItem> : IReusableList<TItem>
{
    private const int DefaultCapacity = 255;

    private readonly Func<int, TItem> _typeFactory;
    private readonly RefAction<TItem> _reuseFactory;

    private TItem[] _array;
    private int _arrayIndex;

    private int[] _reusableSlots;
    private int _reusableSlotsIndex;

    public ReusableList(Func<int, TItem> factory, RefAction<TItem> reuseFactory) : this(factory, reuseFactory, DefaultCapacity)
    { }

    public ReusableList(Func<int, TItem> factory, RefAction<TItem> reuseFactory, int capacity)
    {
        _typeFactory = factory;
        _reuseFactory = reuseFactory;

        _array = new TItem[capacity];
        _arrayIndex = 0;

        _reusableSlots = new int[capacity];
        _reusableSlotsIndex = 0;
    }

    public ref TItem Get(out int id)
    {
        if (_reusableSlotsIndex != 0)
        {
            id = _reusableSlots[--_reusableSlotsIndex];
            _reuseFactory(ref _array[id]);

            return ref _array[id];
        }

        if (_arrayIndex == _array.Length)
            Array.Resize(ref _array, _array.Length * 2);

        id = _arrayIndex++;
        _array[id] = _typeFactory(id);

        return ref _array[id];
    }

    public ref TItem Get(int id) => ref _array[id];

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

    public Span<TItem> AsSpan() => _array.AsSpan();
}