namespace Tea2D.Metrics.Diagnostics;

public sealed class NoOpCounter<T> : ICounter<T>
    where T : struct
{
    public void Add(T value)
    {
    }

    public void Dispose()
    {
    }
}