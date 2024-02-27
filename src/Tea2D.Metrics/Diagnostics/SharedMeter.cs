using Tea2D.Metrics.IO.SharedMemory;

namespace Tea2D.Metrics.Diagnostics;

public sealed class SharedMeter(ReadOnlySpan<char> name) : IMeter
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
}