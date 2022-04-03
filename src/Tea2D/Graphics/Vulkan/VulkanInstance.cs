using System;
using Evergine.Bindings.Vulkan;
using Tea2D.Core.Diagnostics.Logging;

namespace Tea2D.Graphics.Vulkan
{
    public sealed class VulkanInstance : IDisposable
    {
        private readonly VkInstance _instance;
        private readonly VkDebugUtilsMessengerEXT _debugMessenger = VkDebugUtilsMessengerEXT.Null;

        internal VulkanInstance(VkInstance instance)
        {
            _instance = instance;

            VulkanApi.SetupDebugMessenger(new VulkanDebugUtilsMessengerCreateInfo
            {
                LogLevel = LogLevel.Trace,
                GeneralMessageTypeEnabled = true,
                ValidationMessageTypeEnabled = true,
                PerformanceMessageTypeEnabled = true
            }, instance, ref _debugMessenger);
        }

        public unsafe void Dispose()
        {
            VulkanApi.DestroyDebugMessenger(_instance, _debugMessenger);
            VulkanNative.vkDestroyInstance(_instance, null);
        }
    }
}