namespace MetricsVisualizer.Collections;

public sealed class DisposableDictionary<TKey, TItem> : Dictionary<TKey, TItem>, IDisposable
    where TKey : notnull
    where TItem : IDisposable
{
    public void Dispose()
    {
        foreach (var (_, disposable) in this) 
            disposable?.Dispose();
    }
}