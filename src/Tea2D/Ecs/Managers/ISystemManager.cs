using Tea2D.Core.Collections;
using Tea2D.Ecs.Systems;

namespace Tea2D.Ecs.Managers;

internal interface ISystemManager
{
    FastList<ISystem> Systems { get; }

    void RegisterSystem<TSystem>()
        where TSystem : class, ISystem, new();
}