using Tea2D.Ecs.Components;

namespace Tea2D.Benchmarks;

public struct Component<T> : IComponent<Component<T>>
{
    public int Id { get; init; }
    public T Value;

    public void Disable()
    {
        Value = default;
    }
}

public struct HundredByteStruct
{
    // Each double is 8 bytes, so 10 doubles will be 80 bytes
    public double Double1;
    public double Double2;
    public double Double3;
    public double Double4;
    public double Double5;
    public double Double6;
    public double Double7;
    public double Double8;
    public double Double9;
    public double Double10;

    // Each int is 4 bytes, so 5 ints will be 20 bytes
    public int Int1;
    public int Int2;
    public int Int3;
    public int Int4;
    public int Int5;
}