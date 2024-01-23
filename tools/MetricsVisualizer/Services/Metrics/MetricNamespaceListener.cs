using MetricsVisualizer.Services.IO.SharedMemory;
using Tea2D.Metrics.Diagnostics;

namespace MetricsVisualizer.Services.Metrics;

public sealed class MetricNamespaceListener() : SharedRingBufferListener<MetricMetadata>(MetricsNamespace)
{
    private const string MetricsNamespace = "tea2d";
    
    

    protected override void HandleItem(MetricMetadata item)
    {
        
    }
}

public sealed class MetricListener : SharedRingBufferListener<long>
{
    private readonly string _name;
    
    public MetricListener(string name) : base(name)
    {
        _name = name;
    }

    protected override void HandleItem(long item)
    {
        
    }
}