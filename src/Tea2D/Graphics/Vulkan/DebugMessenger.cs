using System;
using System.Diagnostics;
using System.Text;
using Evergine.Bindings.Vulkan;
using Tea2D.Core.Diagnostics.Logging;

namespace Tea2D.Graphics.Vulkan
{
    public sealed class DebugMessenger : IDisposable
    {
        private readonly VkInstance _instance;
        private readonly VkDebugUtilsMessengerEXT _debugMessenger;

        private DebugMessenger(VkInstance instance, VkDebugUtilsMessengerEXT debugMessenger)
        {
            _instance = instance;
            _debugMessenger = debugMessenger;
        }

        [Conditional("DEBUG")]
        // ReSharper disable once RedundantAssignment
        public static void SetupDebugMessenger(ref VkInstance instance, ref DebugMessenger debugMessenger)
        {
            var messengerExt = VkDebugUtilsMessengerEXT.Null;
            
            VulkanApi.SetupDebugMessenger(new VulkanDebugUtilsMessengerCreateInfo
            {
                LogLevel = LogLevel.Trace,
                GeneralMessageTypeEnabled = true,
                ValidationMessageTypeEnabled = true,
                PerformanceMessageTypeEnabled = true,
                CallbackHandler = HandleCallback
            }, instance, ref messengerExt);

            debugMessenger = new DebugMessenger(instance, messengerExt);
        }
        
        private static void HandleCallback(string message, LogLevel logLevel)
        {
            Logger.Instance.Log(in logLevel, $"VulkanApi.DebugCallback: Validation layer `{message}`");
        }

        public void Dispose()
        {
            VulkanApi.DestroyDebugMessenger(in _instance, in _debugMessenger);
        }
    }
}