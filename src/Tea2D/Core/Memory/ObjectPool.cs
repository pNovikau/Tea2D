namespace Tea2D.Core.Memory;

public static class ObjectPool<T>
    where T : class, new()
{
    public static readonly Microsoft.Extensions.ObjectPool.ObjectPool<T> Default = Microsoft.Extensions.ObjectPool.ObjectPool.Create<T>();
}