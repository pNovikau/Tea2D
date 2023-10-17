using Tea2D.Core.Collections;
using Tea2D.Diagnostics;
using Tea2D.Ecs.Systems;

namespace Tea2D.Ecs.Managers;

public class SystemManager : ISystemManager
{
    private readonly FastList<ISystem> _systems = new(255);

    public FastList<ISystem> Systems => _systems;

    public void RegisterSystem<TSystem>(GameContext gameContext)
        where TSystem : class, ISystem, new()
    {
        var system = new TSystem();

        _systems.Add(system);

        Metrics.Systems<TSystem>.Increment();
    }
}