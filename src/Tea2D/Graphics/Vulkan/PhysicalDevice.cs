using Evergine.Bindings.Vulkan;
using Tea2D.Core.Diagnostics.Logging;

namespace Tea2D.Graphics.Vulkan
{
    public sealed class PhysicalDevice
    {
        private readonly VkPhysicalDevice _physicalDevice;

        private PhysicalDevice(VkPhysicalDevice physicalDevice)
        {
            _physicalDevice = physicalDevice;
        }

        public static PhysicalDevice PickPhysicalDevice(ref VkInstance instance)
        {
            var physicalDevices = VulkanApi.EnumeratePhysicalDevices(ref instance);

            if (physicalDevices.Length == 0)
            {
                Logger.Instance.LogFatal("VulkanApi: Failed to find GPUs with Vulkan support");
                //TODO: throw
                return null;
            }

            foreach (var physicalDevice in physicalDevices)
            {
                if (IsDeviceSuitable(in physicalDevice))
                    return new PhysicalDevice(physicalDevice);
            }

            Logger.Instance.LogFatal("VulkanApi: Failed to find a suitable GPU");
            //TODO: throw
            return null;
        }

        private static bool IsDeviceSuitable(in VkPhysicalDevice physicalDevice)
        {
            return true;
        }
    }
}