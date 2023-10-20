using Tea2D.Ecs.Components;

namespace SimpleGame.Components;

public struct LifetimeComponent : IComponent<LifetimeComponent>
{
    public int Id { get; init; }

    public int LifetimeInMilliseconds;
}