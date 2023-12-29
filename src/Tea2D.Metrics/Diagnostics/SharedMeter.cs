using Tea2D.Metrics.IO.SharedMemory;

namespace Tea2D.Metrics.Diagnostics;

public sealed class SharedMeter(string name) : IMeter
{
    private readonly PipeWriter<MetricMetadata> _pipeWriter = new(name);

    public ICounter<TValue> CreateCounter<TValue>(string name) where TValue : struct
    {
        var counter = new SharedCounter<TValue>(name);

        var metadata = new MetricMetadata
        {
            Type = MetricType.Counter
        };
        metadata.SetName(name);

        RegisterMetric(metadata);

        return counter;
    }

    public IHistogram<TValue> CreateHistogram<TValue>(string name) where TValue : struct
    {
        var histogram = new SharedHistogram<TValue>(name);

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