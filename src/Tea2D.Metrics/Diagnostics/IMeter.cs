using System.Runtime.InteropServices;
using Tea2D.Metrics.IO.SharedMemory;

namespace Tea2D.Metrics.Diagnostics;

public interface IMeter
{
    ICounter<TValue> CreateCounter<TValue>(string name)
        where TValue : struct;

    IHistogram<TValue> CreateHistogram<TValue>(string name)
        where TValue : struct;
}

public sealed class Meter(string name) : IMeter
{
    private readonly PipeWriter<MetricCreatedEvent> _pipeWriter = new(name);

    public ICounter<TValue> CreateCounter<TValue>(string name) where TValue : struct
    {
        var counter = new SharedCounter<TValue>(name);

        RegisterMetric(counter);

        return counter;
    }

    public IHistogram<TValue> CreateHistogram<TValue>(string name) where TValue : struct
    {
        var histogram = new SharedHistogram<TValue>(name);

        RegisterMetric(histogram);

        return histogram;
    }

    public void RegisterMetric<T>(T metric)
        where T : IMetric
    {
        if (string.IsNullOrWhiteSpace(metric.Name))
            return;

        _pipeWriter.Write(new MetricCreatedEvent { Name = metric.Name });
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct MetricCreatedEvent
{
    [MarshalAs(UnmanagedType.LPStr, SizeConst = Constants.MaxMetricNameSize)]
    public string Name;
}