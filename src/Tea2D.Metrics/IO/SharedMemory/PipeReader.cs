using System.Diagnostics.CodeAnalysis;
using System.IO.MemoryMappedFiles;

namespace Tea2D.Metrics.IO.SharedMemory;

[SuppressMessage("Interoperability", "CA1416")]
public sealed class PipeReader<T> : SharedRingBuffer<T>
    where T : struct
{
    private readonly int _capacity;
    private readonly int _threshold;

    private int _readerIndex;
    private int _readerCounter;

    public PipeReader(string name)
    {
        MemoryMappedFile = MemoryMappedFile.OpenExisting(name, MemoryMappedFileRights.Read);
        ViewAccessor = MemoryMappedFile.CreateViewAccessor(0, HeaderSize, MemoryMappedFileAccess.Read);

        ViewAccessor.Read<Header>(HeaderPosition, out var header);
        _capacity = header.Capacity;
        _threshold = (int)(header.Capacity * 0.95);

        var memorySize = HeaderSize + ItemSize * _capacity;

        ViewAccessor = MemoryMappedFile.CreateViewAccessor(0, memorySize, MemoryMappedFileAccess.Read);
    }

    public bool Read(out T item)
    {
        item = default;

        ViewAccessor.Read<Header>(HeaderPosition, out var header);

        var (readIndex, readerCounter) = NextReaderIndex(header);

        if (readerCounter >= header.WriterCounter) 
            return false;

        var position = CalculateItemPosition(readIndex);
        ViewAccessor.Read(position, out item);

        _readerIndex = readIndex;
        _readerCounter = readerCounter;

        return true;

    }

    private (int index, int counter) NextReaderIndex(Header header)
    {
        var diff = header.WriterCounter - _readerCounter;
        int index;
        int counter;

        if (diff <= _threshold)
        {
            index = (_readerIndex + 1) % _capacity;
            counter = _readerCounter + 1;
        }
        else
        {
            index = (header.WriterCounter - _threshold) % _capacity;
            counter = (header.WriterCounter - _threshold);
        }

        return (index, counter);
    }
}