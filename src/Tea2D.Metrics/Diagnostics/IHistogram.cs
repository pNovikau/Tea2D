namespace Tea2D.Metrics.Diagnostics;

public interface IHistogram<in T> : IMetric, IDisposable
    where T : struct
{
    void Record(T value);
}