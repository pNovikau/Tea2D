namespace Tea2D.Benchmarks.Systems;

public class CreateSystem : Ecs.Systems.System
{
    private const int MaxEntityCount = 1000;

    public static int EntityCount = 0;

    public override void Update(GameContext context)
    {
        if (EntityCount >= MaxEntityCount)
            return;

        for (; EntityCount <= MaxEntityCount; EntityCount++)
        {
            var entity = context.GameWorld.AddEntity();
            entity.AddComponent<Component<int>>();
            entity.AddComponent<Component<uint>>();
            entity.AddComponent<Component<HundredByteStruct>>();
        }
    }
}