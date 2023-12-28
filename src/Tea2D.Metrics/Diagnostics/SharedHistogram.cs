namespace Tea2D.Metrics.Diagnostics;

public sealed class SharedHistogram<T> : SharedMetric<T>, IHistogram<T>
    where T : struct
{
    public SharedHistogram(string name) : base(name, 125)
    {
        if (name.Length > 125)
            throw new ArgumentException("");
    }

    public void Record(T value) => PipeWriter.Write(value);
}