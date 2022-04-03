using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Evergine.Bindings.Vulkan;
using Tea2D.Core.Diagnostics.Logging;

namespace Tea2D.Graphics.Vulkan
{
    public unsafe delegate VkBool32 DebugCallbackDelegate(VkDebugUtilsMessageSeverityFlagsEXT messageSeverity, VkDebugUtilsMessageTypeFlagsEXT messageType, VkDebugUtilsMessengerCallbackDataEXT pCallbackData, void* pUserData);
    
    //TODO: rework!!!
    internal static unsafe partial class VulkanApi
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate VkResult VkCreateDebugUtilsMessengerExtDelegate(VkInstance instance, VkDebugUtilsMessengerCreateInfoEXT* pCreateInfo, VkAllocationCallbacks* pAllocator, VkDebugUtilsMessengerEXT* pMessenger);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void VkDestroyDebugUtilsMessengerExtDelegate(VkInstance instance, VkDebugUtilsMessengerEXT messenger, VkAllocationCallbacks* pAllocator);
        
        private static DebugCallbackDelegate _callbackDelegate;
        private static VkCreateDebugUtilsMessengerExtDelegate _vkCreateDebugUtilsMessengerExtPtr;
        private static VkDestroyDebugUtilsMessengerExtDelegate _vkDestroyDebugUtilsMessengerExtPtr;

        private static VkResult VkCreateDebugUtilsMessengerExt(VkInstance instance, VkDebugUtilsMessengerCreateInfoEXT* pCreateInfo, VkAllocationCallbacks* pAllocator, VkDebugUtilsMessengerEXT* pMessenger) 
            => _vkCreateDebugUtilsMessengerExtPtr(instance, pCreateInfo, pAllocator, pMessenger);
        private static void VkDestroyDebugUtilsMessengerExt(VkInstance instance, VkDebugUtilsMessengerEXT messenger, VkAllocationCallbacks* pAllocator) 
            => _vkDestroyDebugUtilsMessengerExtPtr(instance, messenger, pAllocator);

        [Conditional("DEBUG")]
        public static void SetupDebugMessenger(VulkanDebugUtilsMessengerCreateInfo info, VkInstance instance, ref VkDebugUtilsMessengerEXT ext)
        {
            _callbackDelegate = info.CallbackHandler;

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
                pfnUserCallback = Marshal.GetFunctionPointerForDelegate(_callbackDelegate)
            };

            var debugMessenger = VkDebugUtilsMessengerEXT.Null;
            var functionPointer = VulkanNative.vkGetInstanceProcAddr(instance, (byte*) Marshal.StringToHGlobalAnsi(nameof(VkCreateDebugUtilsMessengerExt)));
            if (functionPointer == IntPtr.Zero)
                //TODO: log error and throw
                return;

            _vkCreateDebugUtilsMessengerExtPtr = Marshal.GetDelegateForFunctionPointer<VkCreateDebugUtilsMessengerExtDelegate>(functionPointer);
            var result = VkCreateDebugUtilsMessengerExt(instance, &createInfo, null, &debugMessenger);
            VulkanUtils.CheckError(result);

            ext = debugMessenger;
        }

        [Conditional("DEBUG")]
        public static void DestroyDebugMessenger(VkInstance instance, VkDebugUtilsMessengerEXT debugMessenger)
        {
            var functionPointer = VulkanNative.vkGetInstanceProcAddr(instance, (byte*) Marshal.StringToHGlobalAnsi(nameof(VkDestroyDebugUtilsMessengerExt)));
            if (functionPointer == IntPtr.Zero) 
                //TODO: log error and throw
                return;

            _vkDestroyDebugUtilsMessengerExtPtr = Marshal.GetDelegateForFunctionPointer<VkDestroyDebugUtilsMessengerExtDelegate>(functionPointer);
            VkDestroyDebugUtilsMessengerExt(instance, debugMessenger, null);
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