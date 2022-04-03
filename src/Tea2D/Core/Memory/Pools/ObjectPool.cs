using System;
using System.Buffers;
using System.Diagnostics;
using Tea2D.Core.Diagnostics.Logging;

namespace Tea2D.Core.Memory.Pools
{
    //TODO: cover with tests
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

            var fixedIndex = 0;
            var startIndex = 0;
            var endIndex = length - 1;

            while (startIndex < endIndex)
            {
                if (endIndex >= _rentedArray.Length)
                {
                    _logger.Debug($"ObjectPool<{typeof(T).FullName}>: Allocated a new array");

                    return new RentedSpan<T>(new T[length], -1, length);
                }

                if (_rentedArray[endIndex])
                {
                    startIndex = endIndex + 1;
                    endIndex += startIndex;

                    fixedIndex = startIndex;
                }
                else if (_rentedArray[startIndex])
                {
                    ++startIndex;
                    ++endIndex;

                    ++fixedIndex;
                }
                else
                {
                    ++startIndex;
                    --endIndex;
                }
            }

            var span = _array.AsSpan().Slice(fixedIndex, length);
            Array.Fill(_rentedArray, true, fixedIndex, length);

            _logger.Trace($"ObjectPool<{typeof(T).FullName}>: rented an array size of {length} with start index {fixedIndex}");

            return new RentedSpan<T>(span, fixedIndex, length);
        }

        public void Return(in RentedSpan<T> rentedSpan)
        {
            if (rentedSpan.StartIndex == -1)
                return;

            Array.Fill(_rentedArray, false, rentedSpan.StartIndex, rentedSpan.Length);

            _logger.Trace($"ObjectPool<{typeof(T).FullName}>: returned an array size of {rentedSpan.Length} with start index {rentedSpan.StartIndex}");
        }
    }
}