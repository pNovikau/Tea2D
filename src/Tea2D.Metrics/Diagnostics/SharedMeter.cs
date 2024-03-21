using Tea2D.Metrics.IO.SharedMemory;

namespace Tea2D.Metrics.Diagnostics;

public sealed class SharedMeter(ReadOnlySpan<char> name) : IMeter, IDisposable
{
    private readonly PipeWriter<MetricMetadata> _pipeWriter = new(name);

    public ICounter CreateCounter(ReadOnlySpan<char> name)
    {
        var counter = new SharedCounter(name);

        var metadata = new MetricMetadata
        {
            Type = MetricType.Counter,
            Name = name
        };

        RegisterMetric(metadata);

        return counter;
    }

    public IHistogram CreateHistogram(ReadOnlySpan<char> name)
    {
        var histogram = new SharedHistogram(name);

        var metadata = new MetricMetadata
        {
            Type = MetricType.Histogram,
            Name = name
        };

        RegisterMetric(metadata);

        return histogram;
    }

    private void RegisterMetric(MetricMetadata metadata)
    {
        _pipeWriter.Write(metadata);
    }

    //TODO: dispose not calling when application is exiting. So memory mapped file still alive even after application is closed.
    public void Dispose()
    {
        _pipeWriter.Dispose();
    }
}