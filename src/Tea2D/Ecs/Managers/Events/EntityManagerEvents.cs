namespace Tea2D.Ecs.Managers.Events;

public sealed class EntityManagerEvents
{
    public event EntityEventHandler? EntityAdded;
    public event EntityEventHandler? EntityRemoved;
    public event EntityComponentEventHandler? EntityComponentAdded;
    public event EntityComponentEventHandler? EntityComponentRemoved;

    public void OnEntityAdded(EntityEventArgs args) => EntityAdded?.Invoke(ref args);
    public void OnEntityRemoved(EntityEventArgs args) => EntityRemoved?.Invoke(ref args);
    public void OnEntityComponentAdded(EntityComponentEventArgs args) => EntityComponentAdded?.Invoke(ref args);
    public void OnEntityComponentRemoved(EntityComponentEventArgs args) => EntityComponentRemoved?.Invoke(ref args);
}