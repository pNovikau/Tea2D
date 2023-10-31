using System.Diagnostics.Metrics;

namespace Tea2D.Core.Diagnostics;

public static partial class Metrics
{
    private static readonly Meter Meter = new("Tea2D");

    private static readonly Counter<long> TotalComponentsCounter = Meter.CreateCounter<long>("components.total");
    private static readonly Counter<long> TotalEntitiesCounter = Meter.CreateCounter<long>("entities.total");
    private static readonly Counter<long> TotalSystemsCounter = Meter.CreateCounter<long>("systems.total");
}