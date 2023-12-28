using System.Runtime.InteropServices;

namespace Tea2D.Metrics.IO.SharedMemory;

[StructLayout(LayoutKind.Sequential)]
public struct Header
{
    [MarshalAs(UnmanagedType.LPStr, SizeConst = Constants.MaxMetricNameSize)]
    public string Name;
    public int WriteIndex;
    public int WriterCounter;
    public int Capacity;
}