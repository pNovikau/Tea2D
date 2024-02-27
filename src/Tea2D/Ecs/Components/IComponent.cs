namespace Tea2D.Ecs.Components;

public interface IComponent
{
    protected static int TypeCount = 0;

    int Id { get; init; }

    void Disable();
}

public interface IComponent<TComponent> : IComponent
    where TComponent : struct
{
    // ReSharper disable once StaticMemberInGenericType
    private static readonly int _componentType;

    static IComponent()
    {
        _componentType = TypeCount++;
    }

    public static int ComponentType => _componentType;
}