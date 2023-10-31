using System;

namespace Tea2D.Ecs.Managers;

internal interface IEntityManager
{
    ref Entity Create();
    ref Entity Get(int id);
    void Remove(int id);
    Span<Entity> AsSpan();
}