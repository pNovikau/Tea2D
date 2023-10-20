using Tea2D.Core.Collections;
using Tea2D.Diagnostics;
using Tea2D.Ecs.Managers.Events;

namespace Tea2D.Ecs.Managers;

public class EntityManager : IEntityManager
{
    private readonly IUnorderedList<Entity> _list = new UnorderedList<Entity>();

    public EntityManagerEvents Events { get; } = new();

    public ref Entity Create()
    {
        ref var entity = ref _list.Get();

        var eventArgs = new EntityEventArgs(entity.Id);
        Events.OnEntityAdded(eventArgs);

        Metrics.Entities.Increment();

        return ref entity;
    }

    public ref Entity Get(int id) => ref _list.Items[id];

    public void Remove(int id)
    {
        _list.Remove(id);

        var eventArgs = new EntityEventArgs(id);
        Events.OnEntityRemoved(eventArgs);

        Metrics.Entities.Decrement();
    }
}