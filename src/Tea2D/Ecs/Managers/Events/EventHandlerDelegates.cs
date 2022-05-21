namespace Tea2D.Ecs.Managers.Events;

public delegate bool EntityEventHandler(ref EntityEventArgs args);
public delegate bool EntityComponentEventHandler(ref EntityComponentEventArgs args);