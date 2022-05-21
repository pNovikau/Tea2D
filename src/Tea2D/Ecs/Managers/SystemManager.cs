using System.Collections.Generic;
using Tea2D.Ecs.Systems;

namespace Tea2D.Ecs.Managers;

public class SystemManager : ISystemManager
{
    private readonly List<ISystem> _systems = new(255);

    public IReadOnlyList<ISystem> Systems => _systems;

    public void RegisterSystem<TSystem>(GameContext gameContext)
        where TSystem : class, ISystem, new()
    {
        var system = new TSystem();

        _systems.Add(system);
    }
}