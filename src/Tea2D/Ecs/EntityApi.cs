using System.Diagnostics;
using Tea2D.Ecs.Components;
using Tea2D.Ecs.Events;
using Tea2D.Ecs.Managers;

namespace Tea2D.Ecs;

public ref struct EntityApi
{
    private readonly GameWorldBase _gameWorld;

    private readonly int _entityId;

    internal EntityApi(int entityId, GameWorldBase gameWorld)
    {
        Debug.Assert(entityId >= 0);
        Debug.Assert(gameWorld != null);

        _entityId = entityId;
        _gameWorld = gameWorld;
    }

    public ref TComponent AddComponent<TComponent>()
        where TComponent : struct, IComponent<TComponent>
    {
        ref var component = ref _gameWorld.ComponentManager.CreateComponent<TComponent>();

        ref var entity = ref _gameWorld.EntityManager.Get(_entityId);
        entity.Components[IComponent<TComponent>.ComponentType] = component.Id;

        var args = new EntityComponentEventArgs(_entityId, component.Id, IComponent<TComponent>.ComponentType);
        _gameWorld.Events.RaiseEntityComponentAdded(args);

        return ref component;
    }

    public void RemoveComponent<TComponent>()
        where TComponent : struct, IComponent<TComponent>
    {
        ref var entity = ref _gameWorld.EntityManager.Get(_entityId);
        var componentId = entity.Components[IComponent<TComponent>.ComponentType];
        entity.Components[IComponent<TComponent>.ComponentType] = -1;

        _gameWorld.ComponentManager.Delete<TComponent>(componentId);

        var args = new EntityComponentEventArgs(_entityId, componentId, IComponent<TComponent>.ComponentType);
        _gameWorld.Events.RaiseEntityComponentRemoved(args);
    }

    public void RemoveComponent<TComponent>(in TComponent _)
        where TComponent : struct, IComponent<TComponent>
    {
        RemoveComponent<TComponent>();
    }
}