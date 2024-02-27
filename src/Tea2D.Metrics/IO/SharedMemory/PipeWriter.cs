using System.Diagnostics.CodeAnalysis;
using System.IO.MemoryMappedFiles;

namespace Tea2D.Metrics.IO.SharedMemory;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public sealed class PipeWriter<T> : SharedRingBuffer<T>
    where T : struct
{
    private const int DefaultCapacity = 1024;

    private readonly int _capacity;

    public PipeWriter(ReadOnlySpan<char> name, int capacity = DefaultCapacity)
    {
        if (name.Length > Constants.MaxMetricNameSize)
            throw new ArgumentException("");

        _capacity = capacity;
        var memorySize = HeaderSize + ItemSize * capacity;

        //TODO: create new CreateOrOpen method to avoid ToString allocation
        MemoryMappedFile = MemoryMappedFile.CreateOrOpen(name.ToString(), memorySize, MemoryMappedFileAccess.ReadWrite);
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