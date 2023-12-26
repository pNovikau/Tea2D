using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Tea2D.Metrics;

public static partial class Interop
{
    [LibraryImport("winbase.h")]
    internal static partial SafeMemoryMappedViewHandle OpenFileMapping(int desiredAccess, [MarshalAs(UnmanagedType.Bool)] bool inheritHandle, [MarshalAs(UnmanagedType.LPStr)] string name);
}