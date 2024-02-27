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

        TotalComponentsCounter = Meter.CreateCounter("components.total");
        TotalEntitiesCounter = Meter.CreateCounter("entities.total");
        TotalSystemsCounter = Meter.CreateCounter("systems.total");
    }

    private static readonly IMeter Meter;

    private static readonly ICounter TotalComponentsCounter;
    private static readonly ICounter TotalEntitiesCounter;
    private static readonly ICounter TotalSystemsCounter;
}