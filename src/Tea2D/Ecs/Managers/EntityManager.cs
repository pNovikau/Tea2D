using System;
using Tea2D.Core.Collections;
using Tea2D.Core.Diagnostics;

namespace Tea2D.Ecs.Managers;

public class EntityManager : IEntityManager
{
    private readonly IReusableList<Entity> _list;

    public EntityManager()
    {
        _list = new ReusableList<Entity>(
            static (id) => new Entity(id),
            static (ref Entity entity) => entity.Reset());
    }

    public ref Entity Create()
    {
        ref var entity = ref _list.Get(out _);

        Metrics.Entities.Increment();

        return ref entity;
    }

    public ref Entity Get(int id) => ref _list.Get(id);

    public void Remove(int id)
    {
        _list.Remove(id);

        Metrics.Entities.Decrement();
    }

    public Span<Entity> AsSpan() => _list.AsSpan();
}