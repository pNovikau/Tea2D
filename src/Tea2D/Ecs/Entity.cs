using System;

namespace Tea2D.Ecs;

public struct Entity
{
    public readonly int[] Components;

    public Entity(int id)
    {
        Id = id;
        Components = new int[255];
        Array.Fill(Components, -1);
    }

    public int Id { get; init; } = default;

    public void Reset()
    {
        Array.Fill(Components, -1);
    }
}