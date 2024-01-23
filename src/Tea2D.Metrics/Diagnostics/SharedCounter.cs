namespace Tea2D.Metrics.Diagnostics;

public sealed class SharedCounter(string name) : SharedMetric<long>(name, 10), ICounter
{
    public void Add(long value) => PipeWriter.Write(value);
}