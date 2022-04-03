using System;
using System.Diagnostics;
using Evergine.Bindings.Vulkan;

namespace Tea2D.Graphics.Vulkan
{
    public static class VulkanUtils
    {
        [Conditional("DEBUG")]
        public static void CheckError(VkResult result)
        {
            if (result != VkResult.VK_SUCCESS)
            {
                //TODO:
                throw new Exception();
            }
        }
    }
}