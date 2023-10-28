using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;

namespace Tea2D.Core.Diagnostics;

public static partial class Metrics
{
    public static partial class Components<TComponent>
    {
        private static readonly string CounterName = "components." + typeof(TComponent).Name;

        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
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