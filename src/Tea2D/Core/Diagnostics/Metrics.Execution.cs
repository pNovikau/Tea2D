﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tea2D.Core.Memory;
using Tea2D.Metrics.Diagnostics;

namespace Tea2D.Core.Diagnostics;

public static partial class Metric
{
    public static class Execution
    {
        private static readonly Dictionary<int, IHistogram> Histograms = new();

        public static ExecutionScope Record(string method)
        {
            const string prefix = "execution.";

            Span<char> buffer = stackalloc char[prefix.Length + method.Length];
            var histogramName = new ValueString(prefix, buffer);
            histogramName.Append(method);

            if (Histograms.TryGetValue(histogramName.GetHashCode(), out var histogram))
                return new ExecutionScope(histogram);

            histogram = Meter.CreateHistogram(histogramName.ToString());
            Histograms[histogramName.GetHashCode()] = histogram;

            return new ExecutionScope(histogram);
        }

        public readonly ref struct ExecutionScope(IHistogram histogram)
        {
            private readonly long _startTimestamp = Stopwatch.GetTimestamp();

            public void Dispose()
            {
                histogram.Record(Stopwatch.GetElapsedTime(_startTimestamp).Microseconds);
            }
        }
    }
}