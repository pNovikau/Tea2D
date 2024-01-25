using Tea2D.Metrics.Diagnostics;
using Tea2DTrace.Services.Messaging;

namespace Tea2DTrace.Models.Messages;

public record struct MetricUpdatedMessage(string Name, MetricType Type, long Value) : IMessage;