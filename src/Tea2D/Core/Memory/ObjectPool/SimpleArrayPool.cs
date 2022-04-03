using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Tea2D.Core.Diagnostics.Logging;

namespace Tea2D.Core.Memory.ObjectPool
{
    public class SimpleArrayPool<T>
    {
        private readonly ILogger _logger = Logger.Instance;

        public static readonly SimpleArrayPool<T> Instance = new();

        private SortedList<int, FreeSpace> _freeSpaces;
        private T[] _array;

        private SimpleArrayPool()
        {
            _freeSpaces = new SortedList<int, FreeSpace>
            {
                {
                    0,
                    new()
                    {
                        StartIndex = 0,
                        EndIndex = 127
                    }
                }
            };

            _array = new T[128];
        }

        public RentedSpan<T> Rent(int size)
        {
            Debug.Assert(size >= 0, "size >= 0");

            if (size == 0)
                return default;

            var startIndex = RentFreeSpace(size);
            var span = _array.AsSpan().Slice(startIndex, size);

            //TODO: add logs

            return new RentedSpan<T>(span, startIndex, startIndex + size);
        }

        public void Return(in RentedSpan<T> rentedSpan)
        {            
            //TODO: add logs

            _freeSpaces.Add(rentedSpan.StartIndex, new FreeSpace
            {
                StartIndex = rentedSpan.StartIndex,
                EndIndex = rentedSpan.EndIndex
            });
        }

        private int RentFreeSpace(int size)
        {
            var startIndex = -1;
            var endIndex = -1;

            while (true)
            {
                foreach (var (key, freeSpace) in _freeSpaces)
                {
                    var spaceSize = freeSpace.GetSize();

                    if (spaceSize < size) continue;

                    startIndex = key;

                    if (spaceSize == size)
                    {
                        break;
                    }

                    endIndex = freeSpace.EndIndex;

                    break;
                }

                if (startIndex != -1)
                {
                    _freeSpaces.Remove(startIndex);

                    if (endIndex != -1)
                    {
                        _freeSpaces.Add(startIndex + size, new FreeSpace
                        {
                            StartIndex = startIndex + size,
                            EndIndex = endIndex
                        });
                    }

                    return startIndex;
                }

                EnsureCapacity(_array.Length * 2);
            }
        }

        private void EnsureCapacity(int min)
        {
            if (_array.Length >= min)
                return;

            var newCapacity = _array.Length * 2;

            if (newCapacity < min)
                newCapacity = min;

            if (_freeSpaces.Count == 0)
            {
                _freeSpaces.Add(_array.Length, new FreeSpace
                {
                    StartIndex = _array.Length,
                    EndIndex = newCapacity - 1
                });
            }
            else
            {
                var (key, item) = _freeSpaces.Last();

                item.EndIndex = newCapacity - 1;
                _freeSpaces[key] = item;
            }

            Array.Resize(ref _array, min);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FreeSpace
        {
            public int StartIndex;
            public int EndIndex;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int GetSize()
            {
                return EndIndex - StartIndex;
            }
        }
    }
}