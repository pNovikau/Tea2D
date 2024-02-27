using Tea2D.Ecs.Events;
using Tea2D.Ecs.Managers;

namespace Tea2D.Ecs;

internal class GameWorld : GameWorldBase
{
    internal override IEntityManager EntityManager { get; } = new EntityManager();
    internal override ISystemManager SystemManager { get; } = new SystemManager();
    internal override IComponentManager ComponentManager { get; } = new ComponentManager();

    internal override GameWorldEvents Events { get; } = new();

    public override void Initialize(GameContext context)
    {
        foreach (var system in SystemManager.Systems)
            system.Initialize(context);
    }

    public override void Update(GameContext context)
    {
        foreach (var system in SystemManager.Systems)
            system.Update(context);

        context.GameTime.Update();
    }

    public override EntityApi AddEntity()
    {
        ref var entity = ref EntityManager.Create();

        return new EntityApi(entity.Id, this);
    }

    public override EntityApi GetEntity(int entityId)
    {
        return new EntityApi(entityId, this);
    }

    public override void DestroyEntity(int entityId)
    {
        var entity = EntityManager.Get(entityId);

        for (var componentType = 0; componentType < entity.Components.Length; componentType++)
        {
            if (entity.Components[componentType] != -1)
                ComponentManager.DeleteComponent(componentType, entity.Components[componentType]);
        }

        EntityManager.Remove(entityId);

        var args = new EntityEventArgs(entityId); 
        Events?.RaiseEntityRemoved(args);
    }

    public override void RegisterSystem<TSystem>()
    {
        SystemManager.RegisterSystem<TSystem>();
    }
}