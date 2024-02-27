using Tea2D.Ecs;

namespace Tea2D.Benchmarks;

public static class StubEntity
{
    public static EntityApi CreateStub(this GameWorldBase gameWorld)
    {
        var entity = gameWorld.AddEntity();
        entity.AddComponent<Component<int>>();
        entity.AddComponent<Component<uint>>();

        return entity;
    }
}