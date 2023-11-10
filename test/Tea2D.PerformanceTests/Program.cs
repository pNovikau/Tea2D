using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Tea2D.Core;
using Tea2D.Ecs;
using Tea2D.Ecs.ComponentFilters;
using Tea2D.Ecs.Components;
using Tea2D.Ecs.Managers;

namespace Tea2D.PerformanceTests;

internal class Program
{
    public static void Main(string[] args)
    {
        var t = new GameBenchmarks();
        t.Setup();
        t.Run();
        //BenchmarkRunner.Run<GameBenchmarks>();
    }
}

public struct Component<T> : IComponent<Component<T>>
{
    public int Id { get; init; }
    public T Value;

    public void Disable()
    {
        Value = default;
    }
}

public class CreateSystem : Ecs.Systems.System
{
    private const int MaxEntityCount = 1000;

    public static int EntityCount = 0;

    public override void Update(GameContext context)
    {
        if (EntityCount >= MaxEntityCount)
            return;

        for (; EntityCount <= MaxEntityCount; EntityCount++)
        {
            var entity = context.GameWorld.AddEntity();
            entity.AddComponent<Component<int>>();
            entity.AddComponent<Component<uint>>();
        }
    }
}

public class DeleteSystem : Ecs.Systems.System
{
    private ComponentsFilter<Component<int>, Component<uint>> _componentsFilter;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = new ComponentsFilter<Component<int>, Component<uint>>(context.GameWorld);
    }

    public override void Update(GameContext context)
    {
        foreach (var (entityId, component1Ref, component2Ref) in _componentsFilter)
        {
            ref var component1 = ref component1Ref.Value;
            ref var component2 = ref component2Ref.Value;

            if (component1.Value >= 10 && component2.Value >= 10)
                context.GameWorld.DestroyEntity(entityId);

            --CreateSystem.EntityCount;
        }
    }
}

public class UpdateSystem : Ecs.Systems.System
{
    private ComponentsFilter<Component<int>, Component<uint>> _componentsFilter;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = new ComponentsFilter<Component<int>, Component<uint>>(context.GameWorld);
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, component1Ref, component2Ref) in _componentsFilter)
        {
            ref var component1 = ref component1Ref.Value;
            component1.Value += 1;

            ref var component2 = ref component2Ref.Value;
            component2.Value += 1;
        }
    }
}

[MemoryDiagnoser]
public class GameBenchmarks
{
    private const int TicksCount = 120;

    private GameWorldBase _gameWorld;
    private GameTime _gameTime;

    [GlobalSetup]
    public void Setup()
    {
        _gameWorld = Application.CreateGameWorld();
        _gameTime = new GameTime();

        _gameWorld.RegisterSystem<CreateSystem>();
        _gameWorld.RegisterSystem<UpdateSystem>();
        _gameWorld.RegisterSystem<DeleteSystem>();

        _gameWorld.Initialize(new GameContext { GameWorld = _gameWorld });
    }

    [Benchmark]
    public void Run()
    {
        var context = new GameContext
        {
            GameWorld = _gameWorld,
            GameTime = _gameTime
        };

        for (var i = 0; i < TicksCount; i++)
        {
            _gameWorld.Update(context);
        }
    }
}

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