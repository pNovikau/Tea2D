namespace Tea2D.Graphics.Vulkan
{
    public ref struct VulkanInstanceCreateInfo
    {
        public string[] EnabledLayerNames;
        public int EnabledLayerCount => EnabledLayerNames?.Length ?? 0;
        
        public string[] EnabledExtensionNames;
        public int EnabledExtensionCount => EnabledExtensionNames?.Length ?? 0;
    }
}