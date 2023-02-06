using System;
using System.Diagnostics;
using Tea2D.Core.Diagnostics.Logging;

namespace Tea2D.Core.Memory.Pools;

public class ObjectPool<T>
{
    private readonly ILogger _logger = Logger.Instance;

    private readonly T[] _array;
    private readonly bool[] _rentedArray;

    public static readonly ObjectPool<T> Instance = new();

    private ObjectPool()
    {
        _array = new T[1024 * 1024];
        _rentedArray = new bool[1024 * 1024];
    }

    public RentedSpan<T> Rent(int length)
    {
        Debug.Assert(length > 0, "length > 0");

        var rentedSpan = _rentedArray.AsSpan();
        Span<bool> rentedSpaceSpan;
        var index = length * -1;

        do
        {
            index += length;
            rentedSpaceSpan = rentedSpan.Slice(index, length);
        } while (index < _rentedArray.Length && rentedSpaceSpan.LastIndexOf(true) != -1);

        if (index >= _rentedArray.Length)
        {
            _logger.Debug($"ObjectPool<{typeof(T).FullName}>: Allocated a new array");

            return new RentedSpan<T>(new T[length], -1, length);
        }

        var span = _array.AsSpan().Slice(index, length);
        rentedSpaceSpan.Fill(true);

        _logger.Trace($"ObjectPool<{typeof(T).FullName}>: rented an array size of {length} with start index {index}");

        return new RentedSpan<T>(span, index, length);
    }

    public void Return(in RentedSpan<T> rentedSpan)
    {
        if (rentedSpan.StartIndex == -1)
            return;

        _rentedArray.AsSpan().Slice(rentedSpan.StartIndex, rentedSpan.Length).Fill(false);

        _logger.Trace($"ObjectPool<{typeof(T).FullName}>: returned an array size of {rentedSpan.Length} with start index {rentedSpan.StartIndex}");
    }
}