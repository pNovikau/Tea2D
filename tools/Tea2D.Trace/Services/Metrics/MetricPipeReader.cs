using Tea2D.Metrics.Diagnostics;
using Tea2D.Metrics.IO.SharedMemory;

namespace Tea2D.Trace.Services.Metrics;

public sealed record MetricPipeReader<T> : IDisposable
    where T : struct
{
    public MetricPipeReader(string name, MetricType type)
    {
        Name = name;
        Type = type;
        Reader = new PipeReader<T>(name);
    }

    public string Name { get; }
    public MetricType Type { get; }
    public PipeReader<T> Reader { get; }

    public void Dispose()
    {
        Reader.Dispose();
    }
}