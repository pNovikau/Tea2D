using Tea2D.Ecs.Managers.Events;

namespace Tea2D.Ecs.Managers;

public interface IEntityManager
{
    EntityManagerEvents Events { get; }

    ref Entity Create();
    ref Entity Get(int id);
    void Remove(int id);
}