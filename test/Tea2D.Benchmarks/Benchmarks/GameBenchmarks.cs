using BenchmarkDotNet.Attributes;
using Tea2D.Benchmarks.Systems;
using Tea2D.Core;
using Tea2D.Ecs;

namespace Tea2D.Benchmarks.Benchmarks;

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

        _gameWorld.Update(context);
    }
}