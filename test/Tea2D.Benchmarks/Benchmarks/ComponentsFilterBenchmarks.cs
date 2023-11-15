using BenchmarkDotNet.Attributes;
using Tea2D.Ecs;
using Tea2D.Ecs.ComponentFilters;

namespace Tea2D.Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class ComponentsFilterBenchmarks
{
    private const int InitialEntitiesCount = 1000;

    private GameWorldBase _gameWorld;
    private ComponentsFilter<Component<int>, Component<uint>> _componentsFilter;

    [GlobalSetup]
    public void Setup()
    {
        _gameWorld = Application.CreateGameWorld();
        _componentsFilter = new ComponentsFilter<Component<int>, Component<uint>>(_gameWorld);

        for (var i = 0; i < InitialEntitiesCount; i++)
        {
            var entity = _gameWorld.AddEntity();
            entity.AddComponent<Component<int>>();
            entity.AddComponent<Component<uint>>();
        }
    }

    [Benchmark]
    public void ForEach()
    {
        foreach (var (_, component1Ref, component2Ref) in _componentsFilter)
        {
            ref var component1 = ref component1Ref.Value;
            component1.Value += 1;

            ref var component2 = ref component2Ref.Value;
            component2.Value += 1;
        }
    }

    [Benchmark]
    public void ForEach_While_Adding_And_Removing_Entity()
    {
        foreach (var (id, component1Ref, component2Ref) in _componentsFilter)
        {
            ref var component1 = ref component1Ref.Value;
            component1.Value += 1;

            ref var component2 = ref component2Ref.Value;
            component2.Value += 1;

            var entity = _gameWorld.AddEntity();
            entity.AddComponent<Component<int>>();
            entity.AddComponent<Component<uint>>();

            _gameWorld.DestroyEntity(id);
        }
    }
}