using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Tea2D.Metrics.Diagnostics;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct MetricMetadata
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MaxMetricNameSize)]
    private fixed char _name[Constants.MaxMetricNameSize];

    private int _nameLength;

    public MetricType Type;

    public ReadOnlySpan<char> Name
    {
        get => new(Unsafe.AsPointer(ref _name[0]), _nameLength);
        set
        {
            _nameLength = value.Length;

            for (var i = 0; i < value.Length; i++) 
                _name[i] = value[i];
        }
    }
}