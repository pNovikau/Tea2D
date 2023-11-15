using BenchmarkDotNet.Attributes;
using Tea2D.Ecs.Managers;

namespace Tea2D.Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class ComponentManagerBenchmarks
{
    private IComponentManager _componentManager;

    [GlobalSetup]
    public void Setup()
    {
        _componentManager = new ComponentManager();
    }

    [Benchmark]
    public void CreateComponent_And_DeleteComponent()
    {
        ref var comp = ref _componentManager.CreateComponent<Component<int>>();
        _componentManager.DeleteComponent<Component<int>>(comp.Id);
    }

    [Benchmark]
    public void CreateComponent_And_GetComponent()
    {
        ref var comp = ref _componentManager.CreateComponent<Component<int>>();
        _componentManager.GetComponent<Component<int>>(comp.Id);
    }
}