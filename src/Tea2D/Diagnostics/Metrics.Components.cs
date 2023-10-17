using System.Diagnostics.Metrics;

namespace Tea2D.Diagnostics;

public static partial class Metrics
{
    public static partial class Components<TComponent>
    {
        private const string CounterName = "components." + nameof(TComponent);

        private static readonly Counter<long> Counter = Meter.CreateCounter<long>(CounterName);

        public static void Increment()
        {
            Counter.Add(1);
            TotalComponentsCounter.Add(1);
        }

        public static void Decrement()
        {
            Counter.Add(-1);
            TotalComponentsCounter.Add(-1);
        }
    }
}