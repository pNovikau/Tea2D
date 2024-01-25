using Tea2D.Metrics.Diagnostics;
using Tea2D.Metrics.IO.SharedMemory;
using Tea2DTrace.Models.Messages;
using Tea2DTrace.Services.Collections;
using Tea2DTrace.Services.IO.SharedMemory;
using Tea2DTrace.Services.Messaging;

namespace Tea2DTrace.Services.Metrics;

public sealed class MetricNamespaceListener : BackgroundWorker
{
    private const string MetricsNamespace = "tea2d";

    private readonly IMessagePublisher _messagePublisher;

    public MetricNamespaceListener(IMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
    }

    protected override void Run()
    {
        var cancellationToken = Token;

        MemoryMappedFileHelper.WaitForMemoryMappedFile(MetricsNamespace, cancellationToken);

        using var pipeReader = new PipeReader<MetricMetadata>(MetricsNamespace);
        using var metricDictionary = new DisposableDictionary<int, MetricPipeReader<long>>();

        while (cancellationToken.IsCancellationRequested is false)
        {
            while (pipeReader.Read(out var item))
            {
                var metricNameSpan = item.Name;
                var metricName = metricNameSpan.ToString();
                var hashCode = string.GetHashCode(metricNameSpan);

                metricDictionary[hashCode] = new MetricPipeReader<long>(metricName, item.Type);

                _messagePublisher.PublishAsync(new MetricAddedMessage(metricName, item.Type));
            }

            foreach (var (_, metricPipeReader) in metricDictionary)
            {
                if (metricPipeReader.Reader.Read(out var value))
                {
                    _messagePublisher.PublishAsync(new MetricUpdatedMessage(metricPipeReader.Name, metricPipeReader.Type, value));
                }
            }
        }
    }
}