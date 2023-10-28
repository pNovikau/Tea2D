namespace Tea2D.Core.Diagnostics;

public static partial class Metrics
{
    public static partial class Entities
    {
        public static void Increment() => TotalEntitiesCounter.Add(1);
        public static void Decrement() => TotalEntitiesCounter.Add(-1);
    }
}