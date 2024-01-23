using Tea2D.Metrics.IO.SharedMemory;

namespace Tea2D.Metrics.Diagnostics;

public sealed class SharedMeter(string name) : IMeter
{
    private readonly PipeWriter<MetricMetadata> _pipeWriter = new(name);

    public ICounter CreateCounter(string name)
    {
        var counter = new SharedCounter(name);

        var metadata = new MetricMetadata
        {
            Type = MetricType.Counter
        };
        metadata.SetName(name);

        RegisterMetric(metadata);

        return counter;
    }

    public IHistogram CreateHistogram(string name)
    {
        var histogram = new SharedHistogram(name);

        var metadata = new MetricMetadata
        {
            Type = MetricType.Histogram
        };
        metadata.SetName(name);

        RegisterMetric(metadata);

        return histogram;
    }

    private void RegisterMetric(MetricMetadata metadata)
    {
        _pipeWriter.Write(metadata);
    }
}