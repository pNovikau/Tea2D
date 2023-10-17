using System.Diagnostics.Metrics;

namespace Tea2D.Diagnostics;

public static partial class Metrics
{
    public static partial class Systems<TComponent>
    {
        private const string CounterName = "systems." + nameof(TComponent);

        private static readonly Counter<long> Counter = Meter.CreateCounter<long>(CounterName);

        public static void Increment()
        {
            Counter.Add(1);
            TotalSystemsCounter.Add(1);
        }

        public static void Decrement()
        {
            Counter.Add(-1);
            TotalSystemsCounter.Add(-1);
        }
    }
}