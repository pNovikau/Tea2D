using System;
using Evergine.Bindings.Vulkan;

namespace Tea2D.Graphics.Vulkan
{
    public sealed class VulkanInstance : IDisposable
    {
        private readonly VkInstance _instance;
        private readonly PhysicalDevice _physicalDevice;
        private readonly DebugMessenger? _debugMessenger;

        internal VulkanInstance(VkInstance instance)
        {
            _instance = instance;
            _physicalDevice = PhysicalDevice.PickPhysicalDevice(ref instance);

            DebugMessenger.SetupDebugMessenger(ref _instance, ref _debugMessenger!);
        }

        public unsafe void Dispose()
        {
            _debugMessenger?.Dispose();
            VulkanNative.vkDestroyInstance(_instance, null);
        }
    }
}