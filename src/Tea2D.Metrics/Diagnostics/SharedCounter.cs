namespace Tea2D.Metrics.Diagnostics;

public sealed class SharedCounter(ReadOnlySpan<char> name) : SharedMetric<long>(name, 10), ICounter
{
    private long _counter;

    public void Add(long value)
    {
        _counter += value;

        PipeWriter.Write(_counter);
    }
}