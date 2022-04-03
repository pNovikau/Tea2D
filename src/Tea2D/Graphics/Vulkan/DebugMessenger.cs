using System;
using System.Diagnostics;
using System.Text;
using Evergine.Bindings.Vulkan;
using Tea2D.Core.Diagnostics.Logging;

namespace Tea2D.Graphics.Vulkan
{
    //TODO: rework!!!
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
        public static unsafe void SetupDebugMessenger(ref VkInstance instance, ref DebugMessenger debugMessenger)
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
        
        private static unsafe VkBool32 HandleCallback(
            VkDebugUtilsMessageSeverityFlagsEXT messageSeverity,
            VkDebugUtilsMessageTypeFlagsEXT messageType,
            VkDebugUtilsMessengerCallbackDataEXT pCallbackData,
            void* pUserData)
        {
            switch (messageSeverity)
            {
                case VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_VERBOSE_BIT_EXT:
                    Logger.Instance.LogTrace($"VulkanApi.DebugCallback: Validation layer `{GetString(pCallbackData.pMessage)}`");
                    break;
                case VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_INFO_BIT_EXT:
                    Logger.Instance.LogInfo($"VulkanApi.DebugCallback: Validation layer `{GetString(pCallbackData.pMessage)}`");
                    break;
                case VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_WARNING_BIT_EXT:
                    Logger.Instance.LogWarning($"VulkanApi.DebugCallback: Validation layer `{GetString(pCallbackData.pMessage)}`");
                    break;
                default:
                    Logger.Instance.LogError($"VulkanApi.DebugCallback: Validation layer `{GetString(pCallbackData.pMessage)}`");
                    break;
            }

            return false;
        }
        
        private static unsafe string GetString(byte* stringStart)
        {
            var characters = 0;
            while (stringStart[characters] != 0)
                characters++;

            return Encoding.UTF8.GetString(stringStart, characters);
        }

        public void Dispose()
        {
            VulkanApi.DestroyDebugMessenger(_instance, _debugMessenger);
        }
    }
}