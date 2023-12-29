namespace Tea2D.Metrics.Diagnostics;

public interface IMeter
{
    ICounter<TValue> CreateCounter<TValue>(string name)
        where TValue : struct;

    IHistogram<TValue> CreateHistogram<TValue>(string name)
        where TValue : struct;
}