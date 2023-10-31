using Tea2D.Ecs.Events;
using Tea2D.Ecs.Managers;
using Tea2D.Ecs.Systems;

namespace Tea2D.Ecs;

public abstract class GameWorldBase
{
    internal abstract IEntityManager EntityManager { get; }
    internal abstract ISystemManager SystemManager { get; }
    internal abstract IComponentManager ComponentManager { get; }

    internal abstract GameWorldEvents Events { get; }

    public abstract void Initialize(GameContext context);
    public abstract void Update(GameContext context);

    public abstract EntityApi AddEntity();
    public abstract EntityApi GetEntity(int entityId);
    public abstract void DestroyEntity(int entityId);

    public abstract void RegisterSystem<TSystem>() where TSystem : class, ISystem, new();
}