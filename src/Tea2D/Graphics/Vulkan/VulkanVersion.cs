namespace Tea2D.Graphics.Vulkan
{
    public readonly ref struct VulkanVersion
    {
        public readonly uint Major;
        public readonly uint Minor;
        public readonly uint Patch;

        public VulkanVersion(uint major, uint minor, uint patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public uint ToUint() => (Major << 22) | (Minor << 12) | Patch;
    }
}