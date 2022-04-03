namespace Tea2D.Vulkan;

public interface IVulkanLoader
{
    IntPtr Load(out IntPtr getInstanceProcAddr);
}