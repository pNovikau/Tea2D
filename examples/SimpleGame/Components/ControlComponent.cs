using Tea2D.Ecs.Components;

namespace SimpleGame.Components;

public struct ControlComponent : IComponent<ControlComponent>
{
    public int Id { get; init; }
}

public struct GunComponent : IComponent<GunComponent>
{
    public int Id { get; init; }

    public uint Ammo;
    public int FireDelayInMilliseconds;
    public int LastFireTimeInMilliseconds;
}

public struct LifetimeComponent : IComponent<LifetimeComponent>
{
    public int Id { get; init; }

    public int CreateAtInMilliseconds;
    public int LifetimeInMilliseconds;
}

public struct DestroyEntityComponent : IComponent<DestroyEntityComponent>
{
    public int Id { get; init; }
}