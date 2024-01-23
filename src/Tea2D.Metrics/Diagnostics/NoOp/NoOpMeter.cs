namespace Tea2D.Metrics.Diagnostics.NoOp;

public sealed class NoOpMeter : IMeter
{
    public ICounter CreateCounter(string name) => new NoOpCounter();

    public IHistogram CreateHistogram(string name) => new NoOpHistogram();
}