using Tea2D.Ecs.Components;
using Tea2D.Ecs.Managers;

namespace Tea2D.Ecs;

public interface IGameWorld
{
    IEntityManager EntityManager { get; }
    ISystemManager SystemManager { get; }
    IComponentManager ComponentManager { get; }

    void Initialize(GameContext context);

    EntityBuilder AddEntity();
    void DestroyEntity(int entityId);

    void DeleteComponent<TComponent>(int componentId, int entityId) where TComponent : struct, IComponent<TComponent>;
    ref TComponent AddComponent<TComponent>(int entityId) where TComponent : struct, IComponent<TComponent>;
}