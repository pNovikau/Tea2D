using System;

namespace Tea2D.Ecs.Components;

public interface IComponentBucket
{
    void Delete(int id);
}

public interface IComponentBucket<TComponent> : IComponentBucket
{
    ref TComponent CreateComponent();
    ref TComponent GetComponent(int id);
    Span<TComponent> AsSpan();
}