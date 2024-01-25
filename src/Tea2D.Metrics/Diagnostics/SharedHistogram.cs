namespace Tea2D.Metrics.Diagnostics;

public sealed class SharedHistogram : SharedMetric<long>, IHistogram
{
    public SharedHistogram(ReadOnlySpan<char> name) : base(name, 125)
    {
        if (name.Length > 125)
            throw new ArgumentException("");
    }

    public void Record(long value) => PipeWriter.Write(value);
}