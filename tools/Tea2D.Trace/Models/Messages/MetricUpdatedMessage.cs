using Tea2D.Metrics.Diagnostics;
using Tea2D.Trace.Services.Messaging;

namespace Tea2D.Trace.Models.Messages;

public record struct MetricUpdatedMessage(string Name, MetricType Type, long Value) : IMessage;