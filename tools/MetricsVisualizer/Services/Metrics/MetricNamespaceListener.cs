using MetricsVisualizer.Collections;
using MetricsVisualizer.Services.IO.SharedMemory;
using Tea2D.Metrics.Diagnostics;
using Tea2D.Metrics.IO.SharedMemory;

namespace MetricsVisualizer.Services.Metrics;

public sealed class MetricNamespaceListener : BackgroundWorker
{
    private const string MetricsNamespace = "tea2d";

    public event EventHandler<MetricAddedArgs>? MetricAdded;
    public event EventHandler<MetricUpdatedArgs>? MetricUpdated;

    protected override void Run()
    {
        var cancellationToken = Token;

        MemoryMappedFileHelper.WaitForMemoryMappedFile(MetricsNamespace, cancellationToken);

        using var pipeReader = new PipeReader<MetricMetadata>(MetricsNamespace);
        using var metricDictionary = new DisposableDictionary<string, MetricPipeReader<long>>();

        while (cancellationToken.IsCancellationRequested is false)
        {
            while (pipeReader.Read(out var item))
            {
                metricDictionary[item.GetName()] = new MetricPipeReader<long>(item.GetName(), item.Type);

                MetricAdded?.Invoke(this, new MetricAddedArgs(item.GetName(), item.Type));
            }

            foreach (var (name, metricPipeReader) in metricDictionary)
            {
                if (metricPipeReader.Reader.Read(out var item))
                {
                    MetricUpdated?.Invoke(this, new MetricUpdatedArgs(name, metricPipeReader.Type, item));
                }
            }
        }
    }
}

public readonly record struct MetricAddedArgs(string Name, MetricType Type);
public readonly record struct MetricUpdatedArgs(string Name, MetricType Type, long Value);

public sealed record MetricPipeReader<T> : IDisposable
    where T : struct
{
    public MetricPipeReader(string name, MetricType type)
    {
        Type = type;
        Reader = new PipeReader<T>(name);
    }

    public MetricType Type { get; }
    public PipeReader<T> Reader { get; }

    public void Dispose()
    {
        Reader.Dispose();
    }
}