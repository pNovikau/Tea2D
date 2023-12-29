using System.IO;
using System.IO.MemoryMappedFiles;
using Tea2D.Metrics.Diagnostics;
using Tea2D.Metrics.IO.SharedMemory;

namespace MetricsVisualizer;

public sealed class MetricListener : IDisposable
{
    private const string MetricsNamespace = "tea2d";

    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly Task _workerTask;

    private bool _isDisposed;

    public event EventHandler<MetricRegisteredEventArgs> MetricAdded;
    public event EventHandler<MeasureEventArgs> MetricUpdated;

    public MetricListener()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _workerTask = new Task(DoWork, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
    }

    public void Start()
    {
        _workerTask.Start();
    }

    private void DoWork()
    {
        var cancellationToken = _cancellationTokenSource.Token;

        WaitForMemoryMappedFile(MetricsNamespace, cancellationToken);

        using var namespaceReader = new PipeReader<MetricMetadata>(MetricsNamespace);
        using var metrics = Disposable.List<Metric<long>>();

        while (cancellationToken.IsCancellationRequested is false)
        {
            UpdateMetrics(namespaceReader, metrics, cancellationToken);
            ProcessMetrics(metrics, cancellationToken);
        }
    }

    private void ProcessMetrics(IEnumerable<Metric<long>> metrics, CancellationToken cancellationToken)
    {
        foreach (var metric in metrics)
        {
            while (metric.Reader.Read(out var val))
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                MetricUpdated?.Invoke(this, new MeasureEventArgs(metric.Name, val));
            }
        }
    }

    private void UpdateMetrics(PipeReader<MetricMetadata> namespaceReader, ICollection<Metric<long>> metrics, CancellationToken cancellationToken)
    {
        while (namespaceReader.Read(out var metricCreatedEvent))
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            var metric = new Metric<long>(metricCreatedEvent.GetName(), metricCreatedEvent.Type);
            metrics.Add(metric);

            MetricAdded?.Invoke(this, new MetricRegisteredEventArgs { Name = metricCreatedEvent.GetName(), Type = metricCreatedEvent.Type });
        }
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();

        _workerTask.GetAwaiter().GetResult();
        _workerTask.Dispose();
    }

    private static void WaitForMemoryMappedFile(string mapName, CancellationToken cancellationToken)
    {
        while (true)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            try
            {
                using (MemoryMappedFile.OpenExisting(mapName))
                    break;
            }
            catch (FileNotFoundException)
            {
                Thread.Sleep(500);
            }
        }
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;

        _cancellationTokenSource.Dispose();
        _workerTask.Dispose();
        _isDisposed = true;
    }
}


public readonly struct MetricRegisteredEventArgs(string name, MetricType type)
{
    public string Name { get; init; } = name;
    public MetricType Type { get; init; } = type;
}

public readonly struct MeasureEventArgs(string name, long value)
{
    public string Name { get; init; } = name;
    public long Value { get; init; } = value;
}

public sealed class Metric<T> : IDisposable
    where T : struct
{
    public Metric(string name, MetricType type)
    {
        Reader = new PipeReader<T>(name);
        Name = name;
        Type = type;
    }

    public PipeReader<T> Reader { get; }
    public string Name { get; }
    public MetricType Type { get; }

    public void Dispose()
    {
        Reader.Dispose();
    }
}

public static class Disposable
{
    public static DisposableList<T> List<T>() where T : IDisposable => new();

    public sealed class DisposableList<T> : List<T>, IDisposable
        where T : IDisposable
    {
        public DisposableList()
        {
        }

        public void Dispose()
        {
            foreach (var disposable in this)
                disposable?.Dispose();
        }
    }
}