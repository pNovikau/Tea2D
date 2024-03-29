using CommunityToolkit.HighPerformance;
using Tea2D.Ecs.Components;

namespace Tea2D.Ecs.Managers;

internal interface IComponentManager
{
    ref TComponent CreateComponent<TComponent>()
        where TComponent : struct, IComponent<TComponent>;

    ref TComponent GetComponent<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>;

    Ref<TComponent> GetComponentAsRef<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>;

    void Delete<TComponent>(int id)
        where TComponent : struct, IComponent<TComponent>;

    void Delete(int componentType, int id);

    IComponentBucket<TComponent> GetComponentBucket<TComponent>()
        where TComponent : struct, IComponent<TComponent>;
}