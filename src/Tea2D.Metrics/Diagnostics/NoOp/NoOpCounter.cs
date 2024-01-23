namespace Tea2D.Metrics.Diagnostics.NoOp;

public sealed class NoOpCounter : ICounter
{
    public string Name { get; } = string.Empty;

    public void Add(long value)
    {
    }

    public void Dispose()
    {
    }
}