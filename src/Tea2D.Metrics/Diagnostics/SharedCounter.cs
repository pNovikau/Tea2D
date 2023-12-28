namespace Tea2D.Metrics.Diagnostics;

public sealed class SharedCounter<T>(string name) : SharedMetric<T>(name, 10), ICounter<T>
    where T : struct
{
    public void Add(T value) => PipeWriter.Write(value);
}