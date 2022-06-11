namespace Tea2D.Ecs.Systems;

public abstract class System : ISystem
{
    public virtual void Initialize(GameContext context) { }

    public abstract void Update(GameContext context);
}