using System.Runtime.InteropServices;
using System.Text;
using Tea2D.Vulkan.Utils;

namespace Tea2D.Vulkan;

public unsafe partial class VulkanNative
{
    private static IVulkanLoader _loader;
    private static IntPtr _vulkanLibraryPointer = IntPtr.Zero;
    private static VkInstance _vkInstance = VkInstance.Null;

    private static IVulkanLoader Loader
    {
        get
        {
            if (_loader != null)
                return _loader;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                _loader = new WindowsVulkanLoader();

            return _loader;
        }
    }

    public static VkResult Initialize()
    {
        _vulkanLibraryPointer = Loader.Load(out var getInstanceProcAddrPointer);
        _vkGetInstanceProcAddrPtr = (delegate* unmanaged[Stdcall]<VkInstance, byte*, delegate* unmanaged[Stdcall]<void>>)getInstanceProcAddrPointer;

        __vkCreateInstance = (delegate* unmanaged<VkInstanceCreateInfo*, VkAllocationCallbacks*, VkInstance*, VkResult>)LoadFunction(IntPtr.Zero, "vkCreateInstance");
        __vkEnumerateInstanceVersion = (delegate* unmanaged<uint*, VkResult>)LoadFunction(IntPtr.Zero, "vkEnumerateInstanceVersion");
        __vkEnumerateInstanceLayerProperties = (delegate* unmanaged<uint*, VkLayerProperties*, VkResult>)LoadFunction(IntPtr.Zero, "vkEnumerateInstanceLayerProperties");
        __vkEnumerateInstanceExtensionProperties = (delegate* unmanaged<byte*, uint*, VkExtensionProperties*, VkResult>)LoadFunction(IntPtr.Zero, "vkEnumerateInstanceExtensionProperties");
        
        return VkResult.VkSuccess;
    }

    public static void LoadInstance(VkInstance instance)
    {
        _vkInstance = instance;

        LoadFunctions(instance.Handle);
    }

    private static delegate* unmanaged[Stdcall]<VkInstance, byte*, delegate* unmanaged[Stdcall]<void>> _vkGetInstanceProcAddrPtr;
    internal static delegate* unmanaged[Stdcall]<void> GetInstanceProcAddr(VkInstance instance, byte* name)
    {
        return _vkGetInstanceProcAddrPtr(instance, name);
    }

    private static delegate* unmanaged[Stdcall]<void> LoadFunction(IntPtr pointer, string name)
    {
        var stringPtr = StringConverter.ToPointer(name);
        
        return GetInstanceProcAddr(pointer, stringPtr);
    }
}