namespace Tea2D.Ecs.Events;

public record struct EntityComponentEventArgs(int EntityId, int ComponentId, int ComponentType);