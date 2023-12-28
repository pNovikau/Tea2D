namespace Tea2D.Metrics.Diagnostics.NoOp;

public sealed class NoOpMeter : IMeter
{
    public ICounter<TValue> CreateCounter<TValue>(string name) where TValue : struct => new NoOpCounter<TValue>();

    public IHistogram<TValue> CreateHistogram<TValue>(string name) where TValue : struct => new NoOpHistogram<TValue>();
}