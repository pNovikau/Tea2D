using System;
using Evergine.Bindings.Vulkan;

namespace Tea2D.Graphics.Vulkan
{
    internal static unsafe partial class VulkanApi
    {
        public static Span<VkPhysicalDevice> EnumeratePhysicalDevices(ref VkInstance instance)
        {
            uint deviceCount;
            VulkanNative.vkEnumeratePhysicalDevices(instance, &deviceCount, null);
            
            if (deviceCount == 0)
                return Span<VkPhysicalDevice>.Empty;
            
            var physicalDevices = stackalloc VkPhysicalDevice[(int) deviceCount];
            VulkanNative.vkEnumeratePhysicalDevices(instance, &deviceCount, physicalDevices);

            return new Span<VkPhysicalDevice>(physicalDevices, (int) deviceCount);
        }
    }
}