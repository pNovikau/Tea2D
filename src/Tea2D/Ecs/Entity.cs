namespace Tea2D.Ecs;

public record struct Entity() : IHasId
{
    public readonly int[] Components = new int[255];

    public int Id { get; init; } = default;
}