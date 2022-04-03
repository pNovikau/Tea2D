﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Evergine.Bindings.Vulkan;
using Tea2D.Core.Diagnostics.Logging;

namespace Tea2D.Graphics.Vulkan
{
    internal static unsafe class VulkanApi
    {
        private static readonly string[] ValidationLayers = {"VK_LAYER_KHRONOS_validation"};

        public static VkInstance CreateInstance(ref VulkanApplicationInfo applicationInfo, ref VulkanInstanceCreateInfo instanceCreateInfo)
        {
            var vkApplicationInfo = new VkApplicationInfo
            {
                sType = VkStructureType.VK_STRUCTURE_TYPE_APPLICATION_INFO,
                pApplicationName = (byte*) Marshal.StringToHGlobalAnsi(applicationInfo.ApplicationName),
                applicationVersion = applicationInfo.ApplicationVersion.ToUint(),
                pEngineName = (byte*) Marshal.StringToHGlobalAnsi(applicationInfo.EngineName),
                engineVersion = applicationInfo.EngineVersion.ToUint(),
                apiVersion = applicationInfo.ApiVersion.ToUint()
            };

            var vkInstanceCreateInfo = new VkInstanceCreateInfo
            {
                sType = VkStructureType.VK_STRUCTURE_TYPE_INSTANCE_CREATE_INFO,
                pApplicationInfo = &vkApplicationInfo,
                enabledLayerCount = 0
            };

            if (instanceCreateInfo.EnabledExtensionCount != 0)
            {
                var enabledExtensionNamesArray = stackalloc IntPtr[instanceCreateInfo.EnabledExtensionNames.Length];

                for (var i = 0; i < instanceCreateInfo.EnabledExtensionCount; i++)
                {
                    var extension = instanceCreateInfo.EnabledExtensionNames[i];
                    enabledExtensionNamesArray[i] = Marshal.StringToHGlobalAnsi(extension);
                }

                vkInstanceCreateInfo.enabledExtensionCount = (uint) instanceCreateInfo.EnabledExtensionCount;
                vkInstanceCreateInfo.ppEnabledExtensionNames = (byte**) enabledExtensionNamesArray;
            }

            VkInstance vulkanInstance;

            GetAllInstanceExtensionsAvailables();
            EnableValidationLayers(ref vkInstanceCreateInfo);

            var vkCreateInstanceResult = VulkanNative.vkCreateInstance(&vkInstanceCreateInfo, null, &vulkanInstance);
            VulkanUtils.CheckError(vkCreateInstanceResult);

            return vulkanInstance;
        }

        public static string[] GetRequiredInstanceExtensions()
        {
            var extensions = GLFW.Vulkan.GetRequiredInstanceExtensions();

#if DEBUG
            Array.Resize(ref extensions, extensions.Length + 1);
            extensions[^1] = VulkanNative.VK_EXT_DEBUG_UTILS_EXTENSION_NAME;
#endif

            return extensions;
        }


        [Conditional("DEBUG")]
        private static void EnableValidationLayers(ref VkInstanceCreateInfo instanceCreateInfo)
        {
            if (CheckValidationLayerSupport())
            {
                var layersToBytesArray = stackalloc IntPtr[ValidationLayers.Length];

                for (var i = 0; i < ValidationLayers.Length; i++)
                {
                    layersToBytesArray[i] = Marshal.StringToHGlobalAnsi(ValidationLayers[i]);
                }

                instanceCreateInfo.enabledLayerCount = (uint) ValidationLayers.Length;
                instanceCreateInfo.ppEnabledLayerNames = (byte**) layersToBytesArray;
            }
            else
            {
                instanceCreateInfo.enabledLayerCount = 0;
            }
        }

        [Conditional("DEBUG")]
        private static void GetAllInstanceExtensionsAvailables()
        {
            uint extensionCount;
            var result = VulkanNative.vkEnumerateInstanceExtensionProperties(null, &extensionCount, null);
            VulkanUtils.CheckError(result);

            var extensions = stackalloc VkExtensionProperties[(int) extensionCount];
            result = VulkanNative.vkEnumerateInstanceExtensionProperties(null, &extensionCount, extensions);
            VulkanUtils.CheckError(result);

            for (var i = 0; i < extensionCount; i++)
            {
                Logger.Instance.Debug($"VulkanApi: Extension `{GetString(extensions[i].extensionName)}` version `{extensions[i].specVersion}`");
            }
        }
        
        private delegate VkBool32 DebugCallbackDelegate(VkDebugUtilsMessageSeverityFlagsEXT messageSeverity, VkDebugUtilsMessageTypeFlagsEXT messageType, VkDebugUtilsMessengerCallbackDataEXT pCallbackData, void* pUserData);
        private static DebugCallbackDelegate CallbackDelegate = new DebugCallbackDelegate(DebugCallback);

        private static VkBool32 DebugCallback(
            VkDebugUtilsMessageSeverityFlagsEXT messageSeverity, 
            VkDebugUtilsMessageTypeFlagsEXT messageType, 
            VkDebugUtilsMessengerCallbackDataEXT pCallbackData, 
            void* pUserData)
        {
            if (messageSeverity == VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_VERBOSE_BIT_EXT)
                Logger.Instance.LogTrace($"VulkanApi.DebugCallback: Validation layer `{GetString(pCallbackData.pMessage)}`");
            else if (messageSeverity == VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_INFO_BIT_EXT)
                Logger.Instance.LogInfo($"VulkanApi.DebugCallback: Validation layer `{GetString(pCallbackData.pMessage)}`");
            else if (messageSeverity == VkDebugUtilsMessageSeverityFlagsEXT.VK_DEBUG_UTILS_MESSAGE_SEVERITY_WARNING_BIT_EXT)
                Logger.Instance.LogWarning($"VulkanApi.DebugCallback: Validation layer `{GetString(pCallbackData.pMessage)}`");
            else
                Logger.Instance.LogError($"VulkanApi.DebugCallback: Validation layer `{GetString(pCallbackData.pMessage)}`");

            return false;
        }
        
        private static bool CheckValidationLayerSupport()
        {
            uint layerCount;
            var result = VulkanNative.vkEnumerateInstanceLayerProperties(&layerCount, null);
            VulkanUtils.CheckError(result);

            var availableLayers = stackalloc VkLayerProperties[(int) layerCount];
            result = VulkanNative.vkEnumerateInstanceLayerProperties(&layerCount, availableLayers);
            VulkanUtils.CheckError(result);

            for (var i = 0; i < layerCount; i++)
            {
                Logger.Instance.Debug(
                    $"VulkanApi: Validation layer `{GetString(availableLayers[i].layerName)}` version `{availableLayers[i].specVersion}` description `{GetString(availableLayers[i].description)}`");
            }

            for (var i = 0; i < ValidationLayers.Length; i++)
            {
                var layerFound = false;
                var validationLayer = ValidationLayers[i];

                for (var j = 0; j < layerCount; j++)
                {
                    if (validationLayer.Equals(GetString(availableLayers[j].layerName)))
                    {
                        layerFound = true;
                        break;
                    }
                }

                if (!layerFound)
                    return false;
            }

            return true;
        }

        private static string GetString(byte* stringStart)
        {
            var characters = 0;
            while (stringStart[characters] != 0)
                characters++;

            return Encoding.UTF8.GetString(stringStart, characters);
        }
    }
}