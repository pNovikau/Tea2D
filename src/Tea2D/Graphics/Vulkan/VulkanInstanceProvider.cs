using System;

namespace Tea2D.Graphics.Vulkan
{
    public class VulkanInstanceProvider : IVulkanInstanceProvider
    {
        public Lazy<VulkanInstance> Instance { get; } = new(Create);

        private static VulkanInstance Create()
        {
            var applicationInfo = new VulkanApplicationInfo
            {
                ApplicationName = "Tea2D",
                EngineName = "No Engine",
                ApiVersion = new VulkanVersion(1, 2, 0),
                EngineVersion = new VulkanVersion(1, 0, 0),
                ApplicationVersion = new VulkanVersion(1, 0, 0),
            };

            var instanceCreateInfo = new VulkanInstanceCreateInfo
            {
                EnabledExtensionNames = VulkanApi.GetRequiredInstanceExtensions()
            };

            var vulkanInstance = VulkanApi.CreateInstance(ref applicationInfo, ref instanceCreateInfo);

            return new VulkanInstance(vulkanInstance);
        }
    }
}