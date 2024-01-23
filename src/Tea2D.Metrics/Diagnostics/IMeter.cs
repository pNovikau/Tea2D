namespace Tea2D.Metrics.Diagnostics;

public interface IMeter
{
    ICounter CreateCounter(string name);

    IHistogram CreateHistogram(string name);
}