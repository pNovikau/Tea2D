using Tea2D.Metrics.Diagnostics;

namespace Tea2D.Core.Diagnostics;

public static partial class Metric
{
    static Metric()
    {
#if DEBUG
        Meter = new SharedMeter("tea2d");
#else
        Meter = new Tea2D.Metrics.Diagnostics.NoOp.NoOpMeter();
#endif

        TotalComponentsCounter = Meter.CreateCounter<long>("components.total");
        TotalEntitiesCounter = Meter.CreateCounter<long>("entities.total");
        TotalSystemsCounter = Meter.CreateCounter<long>("systems.total");
    }

    private static readonly IMeter Meter;

    private static readonly ICounter<long> TotalComponentsCounter;
    private static readonly ICounter<long> TotalEntitiesCounter;
    private static readonly ICounter<long> TotalSystemsCounter;
}