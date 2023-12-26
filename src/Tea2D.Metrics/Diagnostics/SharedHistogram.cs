namespace Tea2D.Metrics.Diagnostics;

public sealed class SharedHistogram<T>(ReadOnlySpan<char> name) : SharedMeter<T>(name, 125), IHistogram<T>
    where T : struct
{
    public void Record(T value) => PipeWriter.Write(value);
}