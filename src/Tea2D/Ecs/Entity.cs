using System;

namespace Tea2D.Ecs;

public struct Entity : IHasId
{
    public readonly int[] Components;

    public Entity()
    {
        Components = new int[255];
        Array.Fill(Components, -1);
    }

    public int Id { get; init; } = default;
}