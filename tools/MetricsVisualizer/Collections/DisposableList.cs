namespace MetricsVisualizer.Collections;

public sealed class DisposableList<T> : List<T>, IDisposable
    where T : IDisposable
{
    public void Dispose()
    {
        foreach (var disposable in this)
            disposable?.Dispose();
    }
}