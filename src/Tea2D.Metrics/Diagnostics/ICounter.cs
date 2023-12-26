namespace Tea2D.Metrics.Diagnostics;

public interface ICounter<in T> : IDisposable
    where T : struct
{
    void Add(T value);
}