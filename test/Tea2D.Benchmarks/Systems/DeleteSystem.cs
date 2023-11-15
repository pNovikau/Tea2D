using Tea2D.Ecs.ComponentFilters;

namespace Tea2D.Benchmarks.Systems;

public class DeleteSystem : Ecs.Systems.System
{
    private ComponentsFilter<Component<int>, Component<uint>, Component<HundredByteStruct>> _componentsFilter;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = new ComponentsFilter<Component<int>, Component<uint>, Component<HundredByteStruct>>(context.GameWorld);
    }

    public override void Update(GameContext context)
    {
        foreach (var (entityId, component1Ref, component2Ref, _) in _componentsFilter)
        {
            ref var component1 = ref component1Ref.Value;
            ref var component2 = ref component2Ref.Value;

            if (component1.Value >= 10 && component2.Value >= 10)
            {
                context.GameWorld.DestroyEntity(entityId);
                --CreateSystem.EntityCount;
            }
        }
    }
}