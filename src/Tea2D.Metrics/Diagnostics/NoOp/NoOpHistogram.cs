namespace Tea2D.Metrics.Diagnostics.NoOp;

public sealed class NoOpHistogram<T> : IHistogram<T>
    where T : struct
{
    public string Name { get; } = string.Empty;

    public void Record(T value)
    {
    }

    public void Dispose()
    {
    }
}