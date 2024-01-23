using System.Runtime.InteropServices;

namespace Tea2D.Metrics.Diagnostics;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct MetricMetadata
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MaxMetricNameSize)]
    public fixed char Name[Constants.MaxMetricNameSize];

    public MetricType Type;
}

public static unsafe class MetricMetadataExtensions
{
    public static void SetName(ref this MetricMetadata metadata, string value)
    {
        var span = value.AsSpan();

        for (var i = 0; i < span.Length; i++)
        {
            metadata.Name[i] = span[i];
        }
    }

    public static string GetName(ref this MetricMetadata metadata)
    {
        fixed (char* pointer = metadata.Name)
            return new string(pointer);
    }
}