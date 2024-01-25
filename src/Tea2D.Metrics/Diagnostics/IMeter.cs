namespace Tea2D.Metrics.Diagnostics;

public interface IMeter
{
    ICounter CreateCounter(ReadOnlySpan<char> name);

    IHistogram CreateHistogram(ReadOnlySpan<char> name);
}