using Tea2D.Ecs.Components;
using Tea2D.Ecs.Managers;
using Tea2D.Ecs.Managers.Events;

namespace Tea2D.Ecs;

public class GameWorld : IGameWorld
{
    public GameWorld()
    {
        EntityManager = new EntityManager();
        SystemManager = new SystemManager();
        ComponentManager = new ComponentManager();
    }

    public IEntityManager EntityManager { get; }
    public ISystemManager SystemManager { get; }
    public IComponentManager ComponentManager { get; }

    public void Initialize(GameContext context)
    {
        foreach (var system in SystemManager.Systems)
            system.Initialize(context);
    }

    public EntityBuilder AddEntity()
    {
        ref var entity = ref EntityManager.Create();

        return new EntityBuilder(entity.Id, EntityManager, ComponentManager);
    }

    public void DestroyEntity(int entityId)
    {
        ref var entity = ref EntityManager.Get(entityId);

        for (var componentType = 0; componentType < entity.ComponentTypes.Length; componentType++)
        {
            if (entity.ComponentTypes[componentType] == false)
                continue;

            var componentId = entity.Components[componentType];
            ComponentManager.Delete(componentType, componentId);

            var args = new EntityComponentEventArgs(entityId, componentId, componentType);
            EntityManager.Events.OnEntityComponentRemoved(args);
        }

        EntityManager.Remove(entityId);
    }

    public void DeleteComponent<TComponent>(int componentId, int entityId)
        where TComponent : struct, IComponent<TComponent>
    {
        ComponentManager.Delete<TComponent>(componentId);

        var args = new EntityComponentEventArgs(entityId, componentId, IComponent<TComponent>.ComponentType);
        EntityManager.Events.OnEntityComponentRemoved(args);
    }

    public ref TComponent AddComponent<TComponent>(int entityId)
        where TComponent : struct, IComponent<TComponent>
    {
        ref var component = ref ComponentManager.CreateComponent<TComponent>();

        var args = new EntityComponentEventArgs(entityId, component.Id, IComponent<TComponent>.ComponentType);
        EntityManager.Events.OnEntityComponentAdded(args);

        return ref component;
    }
}