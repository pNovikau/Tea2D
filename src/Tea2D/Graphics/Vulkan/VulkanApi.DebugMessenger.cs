using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Evergine.Bindings.Vulkan;
using Tea2D.Core.Diagnostics.Logging;

namespace Tea2D.Graphics.Vulkan
{
    public delegate void DebugCallbackDelegate(string message, LogLevel logLevel);

    internal static unsafe partial class VulkanApi
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate VkResult VkCreateDebugUtilsMessengerExtDelegate(VkInstance instance, VkDebugUtilsMessengerCreateInfoEXT* pCreateInfo, VkAllocationCallbacks* pAllocator, VkDebugUtilsMessengerEXT* pMessenger);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void VkDestroyDebugUtilsMessengerExtDelegate(VkInstance instance, VkDebugUtilsMessengerEXT messenger, VkAllocationCallbacks* pAllocator);

        private static delegate* managed<VkDebugUtilsMessageSeverityFlagsEXT, VkDebugUtilsMessageTypeFlagsEXT, VkDebugUtilsMessengerCallbackDataEXT, void*, VkBool32> _callbackDelegate;

        private static DebugCallbackDelegate _callbackHandler;
        
        private static VkBool32 DebugCallback(
            VkDebugUtilsMessageSeverityFlagsEXT messageSeverity,
            VkDebugUtilsMessageTypeFlagsEXT messageType,
            VkDebugUtilsMessengerCallbackDataEXT pCallbackData,
            void* pUserData)
        {
            switch (messageSeverity)
            {
                case VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_VERBOSE_BIT_EXT:
                    _callbackHandler(GetString(pCallbackData.pMessage), LogLevel.Trace);
                    break;
                case VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_INFO_BIT_EXT:
                    _callbackHandler(GetString(pCallbackData.pMessage), LogLevel.Info);
                    break;
                case VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_WARNING_BIT_EXT:
                    _callbackHandler(GetString(pCallbackData.pMessage), LogLevel.Warning);
                    break;
                default:
                    _callbackHandler(GetString(pCallbackData.pMessage), LogLevel.Error);
                    break;
            }

            return false;
        }
        
        [Conditional("DEBUG")]
        public static void SetupDebugMessenger(VulkanDebugUtilsMessengerCreateInfo info, VkInstance instance, ref VkDebugUtilsMessengerEXT ext)
        {
            _callbackDelegate = &DebugCallback;
            _callbackHandler = info.CallbackHandler;

            VkDebugUtilsMessageTypeFlagsEXT messageType = default;

            if (info.GeneralMessageTypeEnabled)
                messageType |= VkDebugUtilsMessageTypeFlagsEXT.VK_DEBUG_UTILS_MESSAGE_TYPE_GENERAL_BIT_EXT;

            if (info.ValidationMessageTypeEnabled)
                messageType |= VkDebugUtilsMessageTypeFlagsEXT.VK_DEBUG_UTILS_MESSAGE_TYPE_VALIDATION_BIT_EXT;

            if (info.PerformanceMessageTypeEnabled)
                messageType |= VkDebugUtilsMessageTypeFlagsEXT.VK_DEBUG_UTILS_MESSAGE_TYPE_PERFORMANCE_BIT_EXT;

            var createInfo = new VkDebugUtilsMessengerCreateInfoEXT
            {
                sType = VkStructureType.VK_STRUCTURE_TYPE_DEBUG_UTILS_MESSENGER_CREATE_INFO_EXT,
                messageSeverity = ToVkDebugUtilsMessageSeverityFlagsExt(info.LogLevel),
                messageType = messageType,
                pfnUserCallback = new IntPtr(_callbackDelegate)
            };

            var debugMessenger = VkDebugUtilsMessengerEXT.Null;
            var pointer = VulkanNative.vkGetInstanceProcAddr(instance, (byte*) Marshal.StringToHGlobalAnsi("vkCreateDebugUtilsMessengerEXT"));
            if (pointer == IntPtr.Zero)
                //TODO: log error and throw
                return;
            
            var functionPointer = Marshal.GetDelegateForFunctionPointer<VkCreateDebugUtilsMessengerExtDelegate>(pointer);

            var result = functionPointer(instance, &createInfo, null, &debugMessenger);
            VulkanUtils.CheckError(result);

            ext = debugMessenger;
        }

        [Conditional("DEBUG")]
        public static void DestroyDebugMessenger(in VkInstance instance, in VkDebugUtilsMessengerEXT debugMessenger)
        {
            var pointer = VulkanNative.vkGetInstanceProcAddr(instance, (byte*) Marshal.StringToHGlobalAnsi("vkDestroyDebugUtilsMessengerEXT"));
            if (pointer == IntPtr.Zero) 
                //TODO: log error and throw
                return;

            var functionPointer = Marshal.GetDelegateForFunctionPointer<VkDestroyDebugUtilsMessengerExtDelegate>(pointer);
            functionPointer(instance, debugMessenger, null);
        }

        private static VkDebugUtilsMessageSeverityFlagsEXT ToVkDebugUtilsMessageSeverityFlagsExt(LogLevel logLevel)
        {
            const VkDebugUtilsMessageSeverityFlagsEXT allSeverityFlags = VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_VERBOSE_BIT_EXT |
                                                                         VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_WARNING_BIT_EXT |
                                                                         VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_ERROR_BIT_EXT;

            const VkDebugUtilsMessageSeverityFlagsEXT warningErrorSeverityFlags = allSeverityFlags ^ VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_VERBOSE_BIT_EXT;

#pragma warning disable 8509
            return logLevel switch
#pragma warning restore 8509
            {
                LogLevel.Trace => allSeverityFlags,
                LogLevel.Debug => allSeverityFlags,
                LogLevel.Info => allSeverityFlags,
                LogLevel.Warning => warningErrorSeverityFlags,
                LogLevel.Error => VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_ERROR_BIT_EXT,
                LogLevel.Fatal => VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_ERROR_BIT_EXT
            };
        }
    }
}