using System.Runtime.InteropServices;

namespace Tea2D.Metrics.IO.SharedMemory;

[StructLayout(LayoutKind.Sequential)]
public struct Header
{
    public int WriteIndex;
    public int WriterCounter;
    public int Capacity;
}