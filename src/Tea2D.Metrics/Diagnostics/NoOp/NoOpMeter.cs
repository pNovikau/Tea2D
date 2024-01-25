namespace Tea2D.Metrics.Diagnostics.NoOp;

public sealed class NoOpMeter : IMeter
{
    public ICounter CreateCounter(ReadOnlySpan<char> name) => new NoOpCounter();

    public IHistogram CreateHistogram(ReadOnlySpan<char> name) => new NoOpHistogram();
}