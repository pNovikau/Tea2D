using Tea2D.Ecs.Managers;

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

    public EntityApi AddEntity()
    {
        ref var entity = ref EntityManager.Create();

        return new EntityApi(entity.Id, EntityManager, ComponentManager);
    }

    public EntityApi GetEntity(int entityId)
    {
        return new EntityApi(entityId, EntityManager, ComponentManager);
    }
}