using Tea2D.Core.Collections;
using Tea2D.Ecs.Systems;

namespace Tea2D.Ecs.Managers;

public interface ISystemManager
{
    FastList<ISystem> Systems { get; }

    void RegisterSystem<TSystem>(GameContext gameContext) where TSystem : class, ISystem, new();
}