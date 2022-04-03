using Tea2D.Core.Diagnostics.Logging;

namespace Tea2D.Graphics.Vulkan
{
    public ref struct VulkanDebugUtilsMessengerCreateInfo
    {
        public LogLevel LogLevel;

        public bool GeneralMessageTypeEnabled;
        public bool ValidationMessageTypeEnabled;
        public bool PerformanceMessageTypeEnabled;

        public DebugCallbackDelegate CallbackHandler;
    }
}