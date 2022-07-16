using Tea2D.Common.Collections;
using Tea2D.Ecs.Managers.Events;

namespace Tea2D.Ecs.Managers;

public class EntityManager : IEntityManager
{
    private readonly IUnorderedList<Entity> _list;

    public EntityManager()
    {
        _list = new UnorderedList<Entity>();

        Events.EntityComponentAdded += OnEntityComponentAdded;
        Events.EntityComponentRemoved += OnEntityComponentRemoved;
    }

    public EntityManagerEvents Events { get; } = new();

    public ref Entity Create()
    {
        ref var entity = ref _list.Get();

        var eventArgs = new EntityEventArgs(entity.Id);
        Events.OnEntityAdded(eventArgs);

        return ref entity;
    }

    public ref Entity Get(int id) => ref _list.Items[id];

    public void Remove(int id)
    {
        _list.Remove(id);

        var eventArgs = new EntityEventArgs(id);
        Events.OnEntityRemoved(eventArgs);
    }

    private bool OnEntityComponentAdded(ref EntityComponentEventArgs args)
    {
        ref var entity = ref Get(args.EntityId);
        entity.Components[args.ComponentType] = args.ComponentId;
        entity.ComponentTypes[args.ComponentType] = true;

        return true;
    }

    private bool OnEntityComponentRemoved(ref EntityComponentEventArgs args)
    {
        ref var entity = ref Get(args.EntityId);
        entity.ComponentTypes[args.ComponentType] = false;

        return true;
    }
}