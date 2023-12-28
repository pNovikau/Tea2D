using System.IO.MemoryMappedFiles;

namespace Tea2D.Metrics.IO.SharedMemory;

public sealed class PipeWriter<T> : SharedRingBuffer<T>
    where T : struct
{
    private const int DefaultCapacity = 1024;

    private readonly int _capacity;

    public PipeWriter(string name, int capacity = DefaultCapacity)
    {
        if (name.Length > Constants.MaxMetricNameSize)
            throw new ArgumentException("");

        _capacity = capacity;
        var memorySize = HeaderSize + ItemSize * capacity;

        MemoryMappedFile = MemoryMappedFile.CreateNew(name, memorySize, MemoryMappedFileAccess.ReadWrite);
        ViewAccessor = MemoryMappedFile.CreateViewAccessor(0, memorySize, MemoryMappedFileAccess.ReadWrite);

        var header = new Header()
        {
            Capacity = capacity
        };
        ViewAccessor.Write(HeaderPosition, ref header);
    }

    public void Write(T item)
    {
        ViewAccessor.Read<Header>(HeaderPosition, out var header);

        header.WriteIndex = (header.WriteIndex + 1) % _capacity;
        header.WriterCounter++;
        ViewAccessor.Write(HeaderPosition, ref header);

        var position = CalculateItemPosition(header.WriteIndex);
        ViewAccessor.Write(position, ref item);
    }
}