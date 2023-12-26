using System.Diagnostics.CodeAnalysis;
using Tea2D.Metrics.Diagnostics;

namespace Tea2D.Core.Diagnostics;

public static partial class Metric
{
    public static class Components<TComponent>
    {
        private static readonly string CounterName = "components." + typeof(TComponent).Name;

        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        private static readonly ICounter<long> Counter;

        static Components()
        {
#if DEBUG
            Counter = new SharedCounter<long>(CounterName);
#else
            _counter = new NoOpCounter<long>();
#endif
        }

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