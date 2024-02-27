namespace Tea2D.Metrics.Diagnostics;

public interface IHistogram : IMetric, IDisposable
{
    void Record(long value);
}