using Tea2D.Metrics.IO.SharedMemory;

namespace Tea2D.Metrics.Diagnostics;

public abstract class SharedMetric<T>(ReadOnlySpan<char> name, int capacity = 125) : IDisposable
    where T : struct
{
    protected readonly PipeWriter<T> PipeWriter = new(name, capacity);

    public void Dispose()
    {
        PipeWriter.Dispose();

        GC.SuppressFinalize(this);
    }
}