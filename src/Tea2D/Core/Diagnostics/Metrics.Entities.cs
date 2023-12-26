namespace Tea2D.Core.Diagnostics;

public static partial class Metric
{
    public static class Entities
    {
        public static void Increment() => TotalEntitiesCounter.Add(1);
        public static void Decrement() => TotalEntitiesCounter.Add(-1);
    }
}