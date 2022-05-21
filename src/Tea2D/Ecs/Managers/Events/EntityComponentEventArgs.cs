namespace Tea2D.Ecs.Managers.Events;

public record struct EntityComponentEventArgs(int EntityId, int ComponentId, int ComponentType);