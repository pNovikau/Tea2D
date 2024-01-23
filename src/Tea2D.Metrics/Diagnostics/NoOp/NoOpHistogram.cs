namespace Tea2D.Metrics.Diagnostics.NoOp;

public sealed class NoOpHistogram : IHistogram
{
    public string Name { get; } = string.Empty;

    public void Record(long value)
    {
    }

    public void Dispose()
    {
    }
}