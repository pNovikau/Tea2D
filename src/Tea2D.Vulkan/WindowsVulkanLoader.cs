using System.Runtime.InteropServices;

namespace Tea2D.Vulkan;

internal class WindowsVulkanLoader : IVulkanLoader
{
    public IntPtr Load(out IntPtr getInstanceProcAddr)
    {
        var pointer = Kernel32.LoadLibrary("vulkan-1.dll");
        getInstanceProcAddr = Kernel32.GetProcAddress(pointer, "vkGetInstanceProcAddr");

        return pointer;
    }

    private static class Kernel32
    {
        [DllImport("kernel32")]
        public static extern IntPtr LoadLibrary(string fileName);

        [DllImport("kernel32")]
        public static extern IntPtr GetProcAddress(IntPtr module, string procName);
    }
}