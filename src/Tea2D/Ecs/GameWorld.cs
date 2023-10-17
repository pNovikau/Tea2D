using Tea2D.Ecs.Managers;

namespace Tea2D.Ecs;

public class GameWorld : IGameWorld
{
    public IEntityManager EntityManager { get; } = new EntityManager();
    public ISystemManager SystemManager { get; } = new SystemManager();
    public IComponentManager ComponentManager { get; } = new ComponentManager();

    public void Initialize(GameContext context)
    {
        foreach (var system in SystemManager.Systems)
            system.Initialize(context);
    }

    public void Update(GameContext context)
    {
        foreach (var system in SystemManager.Systems)
            system.Update(context);

        context.GameTime.Update();
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