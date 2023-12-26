using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tea2D.Core.Memory;
using Tea2D.Metrics.Diagnostics;

namespace Tea2D.Core.Diagnostics;

public static partial class Metric
{
    public static class Execution
    {
        private static readonly Dictionary<int, IHistogram<long>> Histograms = new();

        public static ExecutionScope Record(string method)
        {
            const string prefix = "execution.";

            Span<char> buffer = stackalloc char[prefix.Length + method.Length];
            var histogramName = new ValueString(prefix, buffer);
            histogramName.Append(method);

            if (Histograms.TryGetValue(histogramName.GetHashCode(), out var histogram))
                return new ExecutionScope(histogram);

            histogram = CreateHistogram<long>(histogramName);
            Histograms[histogramName.GetHashCode()] = histogram;

            return new ExecutionScope(histogram);
        }

        private static IHistogram<T> CreateHistogram<T>(ReadOnlySpan<char> name)
            where T : struct
        {
#if DEBUG
            return new SharedHistogram<T>(name);
#else
            return new NoOpHistogram<T>();
#endif
        }

        public readonly ref struct ExecutionScope(IHistogram<long> histogram)
        {
            private readonly long _startTimestamp = Stopwatch.GetTimestamp();

            public void Dispose()
            {
                histogram.Record(Stopwatch.GetElapsedTime(_startTimestamp).Microseconds);
            }
        }
    }
}