using Tea2D.Metrics.Diagnostics;

namespace Tea2D.Core.Diagnostics;

public static partial class Metric
{
    static Metric()
    {
#if DEBUG
        Meter = new Meter("tea2d");

        TotalComponentsCounter = Meter.CreateCounter<long>("components.total");
        TotalEntitiesCounter = Meter.CreateCounter<long>("entities.total");
        TotalSystemsCounter = Meter.CreateCounter<long>("systems.total");
#else
        _totalComponentsCounter = new NoOpCounter<long>();
        _totalEntitiesCounter = new NoOpCounter<long>();
        _totalSystemsCounter = new NoOpCounter<long>();
#endif
    }

    private static readonly IMeter Meter;

    private static readonly ICounter<long> TotalComponentsCounter;
    private static readonly ICounter<long> TotalEntitiesCounter;
    private static readonly ICounter<long> TotalSystemsCounter;
}