using System;

namespace Tea2D.Graphics.Vulkan
{
    public interface IVulkanInstanceProvider
    {
        Lazy<VulkanInstance> Instance { get; }
    }
}