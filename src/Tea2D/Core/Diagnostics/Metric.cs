using Tea2D.Metrics.Diagnostics;

namespace Tea2D.Core.Diagnostics;

public static partial class Metric
{
    static Metric()
    {
#if DEBUG
        TotalComponentsCounter = new SharedCounter<long>("components.total");
        TotalEntitiesCounter = new SharedCounter<long>("entities.total");
        TotalSystemsCounter = new SharedCounter<long>("systems.total");
#else
        _totalComponentsCounter = new NoOpCounter<long>();
        _totalEntitiesCounter = new NoOpCounter<long>();
        _totalSystemsCounter = new NoOpCounter<long>();
#endif
    }

    private static readonly ICounter<long> TotalComponentsCounter;
    private static readonly ICounter<long> TotalEntitiesCounter;
    private static readonly ICounter<long> TotalSystemsCounter;
}