using System.IO.MemoryMappedFiles;
using System.Runtime.CompilerServices;

namespace Tea2D.Metrics.IO.SharedMemory;

public abstract class SharedRingBuffer : IDisposable
{
    protected static readonly int HeaderSize = Unsafe.SizeOf<Header>();

    public abstract void Dispose();
}

public abstract class SharedRingBuffer<T> : SharedRingBuffer
{
    protected const int HeaderPosition = 0;

    protected static readonly int ItemSize = Unsafe.SizeOf<T>();

    protected MemoryMappedFile MemoryMappedFile = null!;
    protected MemoryMappedViewAccessor ViewAccessor = null!;

    protected int CalculateItemPosition(int index) => HeaderSize + index * ItemSize;

    public override void Dispose()
    {
        MemoryMappedFile.Dispose();
        ViewAccessor.Dispose();

        GC.SuppressFinalize(this);
    }
}