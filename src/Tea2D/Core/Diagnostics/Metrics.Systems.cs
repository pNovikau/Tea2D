using System.Diagnostics.CodeAnalysis;
using Tea2D.Metrics.Diagnostics;

namespace Tea2D.Core.Diagnostics;

public static partial class Metric
{
    public static class Systems<TComponent>
    {
        private const string CounterName = "systems." + nameof(TComponent);

        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        private static readonly ICounter<long> Counter = Meter.CreateCounter<long>(CounterName);

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