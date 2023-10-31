using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Tea2D.Core.Memory;

namespace Tea2D.Core.Diagnostics;

public static partial class Metrics
{
    public static partial class Execution
    {
        private static readonly Dictionary<int, Histogram<long>> Histograms = new();

        public static ExecutionScope Record(string method)
        {
            const string prefix = "execution.";

            Span<char> buffer = stackalloc char[prefix.Length + method.Length];
            var histogramName = new ValueString(prefix, buffer);
            histogramName.Append(method);

            if (Histograms.TryGetValue(histogramName.GetHashCode(), out var histogram) is false)
            {
                histogram = Meter.CreateHistogram<long>(histogramName.ToString()!);
                Histograms[histogramName.GetHashCode()] = histogram;
            }

            return new ExecutionScope(histogram);
        }

        public readonly ref struct ExecutionScope
        {
            private readonly long _startTimestamp;
            private readonly Histogram<long> _histogram;

            public ExecutionScope(Histogram<long> histogram)
            {
                _startTimestamp = Stopwatch.GetTimestamp();
                _histogram = histogram;
            }

            public void Dispose()
            {
                _histogram.Record(Stopwatch.GetElapsedTime(_startTimestamp).Microseconds);
            }
        }
    }
}