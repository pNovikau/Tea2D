namespace Tea2D.Metrics.Diagnostics.NoOp;

public sealed class NoOpCounter<T> : ICounter<T>
    where T : struct
{
    public string Name { get; } = string.Empty;

    public void Add(T value)
    {
    }

    public void Dispose()
    {
    }
}