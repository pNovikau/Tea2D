using Tea2D.Ecs.Managers;

namespace Tea2D.Ecs;

public interface IGameWorld
{
    IEntityManager EntityManager { get; }
    ISystemManager SystemManager { get; }
    IComponentManager ComponentManager { get; }

    void Initialize(GameContext context);
    EntityBuilder AddEntity();
}