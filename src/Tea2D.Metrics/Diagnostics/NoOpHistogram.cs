namespace Tea2D.Metrics.Diagnostics;

public sealed class NoOpHistogram<T> : IHistogram<T>
    where T : struct
{
    public void Record(T value)
    {
    }

    public void Dispose()
    {
    }
}