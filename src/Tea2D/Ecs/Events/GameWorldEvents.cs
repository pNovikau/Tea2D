namespace Tea2D.Ecs.Events;

public sealed class GameWorldEvents
{
    public event EntityEventHandler EntityAdded;
    public event EntityEventHandler EntityRemoved;
    public event EntityComponentEventHandler EntityComponentAdded;
    public event EntityComponentEventHandler EntityComponentRemoved;

    public void RaiseEntityAdded(EntityEventArgs args) => EntityAdded?.Invoke(ref args);
    public void RaiseEntityRemoved(EntityEventArgs args) => EntityRemoved?.Invoke(ref args);
    public void RaiseEntityComponentAdded(EntityComponentEventArgs args) => EntityComponentAdded?.Invoke(ref args);
    public void RaiseEntityComponentRemoved(EntityComponentEventArgs args) => EntityComponentRemoved?.Invoke(ref args);
}