using CppAst;

namespace Tea2D.Vulkan.Generator;

//TODO: figure out how convert `typedef struct`
//TODO: rework CSharp elements behaviour
//TODO: create CSharp compilation instead of working with CSharp elements directly

public static class Program
{
    public static void Main(string[] args)
    {
        var tea2DVulkanProjectPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Tea2D.Vulkan", "Generated");
        var headerDirectoryPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "external_lib", "Vulkan-Headers");

        var headerFile = Path.Combine(headerDirectoryPath, "include", "vulkan", "vulkan.h");
        var compilation = CppParser.ParseFile(headerFile);

        var vulkanGlobalUsingsFile = Path.Combine(tea2DVulkanProjectPath, "Vulkan.GlobalUsings.generated.cs");
        GlobalUsingGenerator.Generate(compilation, vulkanGlobalUsingsFile);

        var vulkanStructsFile = Path.Combine(tea2DVulkanProjectPath, "VulkanStructs.generated.cs");
        StructGenerator.Generate(compilation, vulkanStructsFile);

        var vulkanEnumsFile = Path.Combine(tea2DVulkanProjectPath, "VulkanEnums.generated.cs");
        EnumGenerator.Generate(compilation, vulkanEnumsFile);

        var vulkanHandlersFile = Path.Combine(tea2DVulkanProjectPath, "VulkanHandlers.generated.cs");
        HandlersGenerator.Generate(compilation, vulkanHandlersFile);

        var vulkanApiFile = Path.Combine(tea2DVulkanProjectPath, "VulkanNative.generated.cs");
        FunctionsGenerator.Generate(compilation, vulkanApiFile);
    }
}