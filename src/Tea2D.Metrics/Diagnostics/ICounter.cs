namespace Tea2D.Metrics.Diagnostics;

public interface ICounter : IMetric, IDisposable
{
    void Add(long value);
}