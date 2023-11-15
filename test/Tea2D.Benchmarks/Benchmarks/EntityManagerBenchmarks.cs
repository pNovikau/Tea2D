using BenchmarkDotNet.Attributes;
using Tea2D.Ecs.Managers;

namespace Tea2D.Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class EntityManagerBenchmarks
{
    private IEntityManager _entityManager;

    [GlobalSetup]
    public void Setup()
    {
        _entityManager = new EntityManager();
    }

    [Benchmark]
    public void CreateEntity_And_DeleteEntity()
    {
        ref var entity = ref _entityManager.Create();
        _entityManager.Remove(entity.Id);
    }

    [Benchmark]
    public void CreateEntity_And_GetEntity()
    {
        ref var entity = ref _entityManager.Create();
        entity = ref _entityManager.Get(entity.Id);
    }
}