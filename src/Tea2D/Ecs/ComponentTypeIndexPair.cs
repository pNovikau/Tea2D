namespace Tea2D.Ecs;

public record struct ComponentTypeIndexPair(int Type = default, int Index = default)
{
    public readonly int Type = Type;
    public readonly int Index = Index;
}