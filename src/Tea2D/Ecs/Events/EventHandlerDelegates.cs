namespace Tea2D.Ecs.Events;

public delegate bool EntityEventHandler(ref EntityEventArgs args);
public delegate bool EntityComponentEventHandler(ref EntityComponentEventArgs args);