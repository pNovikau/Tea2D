using System;
using System.IO;
using CppAst;

namespace Tea2D.Vulkan.Generator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string tea2DVulkanProjectPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Tea2D.Vulkan");

            var headerFile = Path.Combine(AppContext.BaseDirectory, "include", "vulkan", "vulkan.h");
            var compilation = CppParser.ParseFile(headerFile);

            var vulkanStructsFile = Path.Combine(tea2DVulkanProjectPath, "VulkanStructs.generated.cs");
            StructGenerator.Generate(compilation, vulkanStructsFile);

            var vulkanEnumsFile = Path.Combine(tea2DVulkanProjectPath, "VulkanEnums.generated.cs");
            EnumGenerator.Generate(compilation, vulkanEnumsFile);

            var vulkanHandlersFile = Path.Combine(tea2DVulkanProjectPath, "VulkanHandlers.generated.cs");
            HandlersGenerator.Generate(compilation, vulkanHandlersFile);
        }
    }
}