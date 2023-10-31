// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Tea2D;
using Tea2D.Ecs;
using Tea2D.Ecs.Components;

internal class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ComponentManagerBenchmarks>();
    }
}

file struct Component1 : IComponent<Component1>
{
    public int Id { get; init; }
    public void Disable() { }
}

public class ComponentFilterManagerBenchmarks
{
    
}

[MemoryDiagnoser]
public class ComponentManagerBenchmarks
{
    private GameWorldBase _gameWorld;

    [GlobalSetup]
    public void Setup()
    {
        _gameWorld = Application.CreateGameWorld();
    }
    
    [Benchmark()]
    public void CreateComponent_And_DeleteComponent()
    {
        ref var comp = ref _gameWorld.ComponentManager.CreateComponent<Component1>();
        _gameWorld.ComponentManager.DeleteComponent<Component1>(comp.Id);
    }

    [Benchmark()]
    public void CreateComponent_And_GetComponent()
    {
        ref var comp = ref _gameWorld.ComponentManager.CreateComponent<Component1>();
        _gameWorld.ComponentManager.GetComponent<Component1>(comp.Id);
    }
}