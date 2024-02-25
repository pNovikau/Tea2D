﻿using System.Diagnostics.CodeAnalysis;
using Tea2D.Metrics.Diagnostics;

namespace Tea2D.Core.Diagnostics;

public static partial class Metric
{
    public static class Systems<TComponent>
    {
        private static readonly string CounterName = "systems." + typeof(TComponent).Name;

        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        private static readonly ICounter Counter = Meter.CreateCounter(CounterName);

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