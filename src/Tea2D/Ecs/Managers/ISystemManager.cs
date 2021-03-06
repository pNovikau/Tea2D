using System.Collections.Generic;
using Tea2D.Ecs.Systems;

namespace Tea2D.Ecs.Managers;

public interface ISystemManager
{
    IReadOnlyList<ISystem> Systems { get; }

    void RegisterSystem<TSystem>(GameContext gameContext) where TSystem : class, ISystem, new();
}