using Tea2D.Ecs.ComponentFilters;

namespace Tea2D.Benchmarks.Systems;

public class UpdateSystem : Ecs.Systems.System
{
    private ComponentsFilter<Component<int>, Component<uint>, Component<HundredByteStruct>> _componentsFilter;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = new ComponentsFilter<Component<int>, Component<uint>, Component<HundredByteStruct>>(context.GameWorld);
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, component1Ref, component2Ref, component3Ref) in _componentsFilter)
        {
            ref var component1 = ref component1Ref.Value;
            component1.Value += 1;

            ref var component2 = ref component2Ref.Value;
            component2.Value += 1;

            ref var component3 = ref component3Ref.Value;
            component3.Value.Double1 += 1;
        }
    }
}