using System;
using Evergine.Bindings.Vulkan;

namespace Tea2D.Graphics.Vulkan
{
    public class VulkanInstance : IDisposable
    {
        private readonly VkInstance _instance;

        internal VulkanInstance(VkInstance instance)
        {
            _instance = instance;
        }

        public unsafe void Dispose()
        {
            VulkanNative.vkDestroyInstance(_instance, null);
        }
    }
}