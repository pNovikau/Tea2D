using Microsoft.Extensions.ObjectPool;

namespace Tea2D.Core.Memory;

public static class ObjectPool<T>
    where T : class, new()
{
    public static readonly Microsoft.Extensions.ObjectPool.ObjectPool<T> Default = new DefaultObjectPool<T>(new DefaultPooledObjectPolicy<T>(), 1024);
    //public static readonly Microsoft.Extensions.ObjectPool.ObjectPool<T> Default = Microsoft.Extensions.ObjectPool.ObjectPool.Create<T>();
}