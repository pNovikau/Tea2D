using CppAst;
using Tea2D.Vulkan.Generator.CSharp;

namespace Tea2D.Vulkan.Generator;

public static class FunctionsGenerator
{
    public static void Generate(CppCompilation compilation, string outputFile)
    {
        using var sourceWriter = new SourceFileWriter(outputFile);

        sourceWriter.WriteLine("using System.Diagnostics;");
        sourceWriter.WriteLine("using System.Runtime.InteropServices;");
        sourceWriter.WriteLine();

        sourceWriter.WriteLine("namespace Tea2D.Vulkan;");
        sourceWriter.WriteLine();

        using var _ = sourceWriter.BeginBlock("unsafe partial class VulkanNative");

        var csharpFunctions = new List<CSharpStaticFunction>(compilation.Functions.Count);

        foreach (var cppFunction in compilation.Functions)
        {
            if (!cppFunction.Name.StartsWith("vk"))
                continue;

            if (cppFunction.Name.Equals("vkGetInstanceProcAddr") ||
                cppFunction.Name.Equals("vkGetDeviceProcAddr"))
                continue;

            
            var function = new CSharpStaticFunction(cppFunction);
            csharpFunctions.Add(function);

            function.Write(sourceWriter);
            sourceWriter.WriteLine();
        }
        
        using var __ = sourceWriter.BeginBlock("private static void LoadFunctions(IntPtr instance)");

        foreach (var function in csharpFunctions.OrderBy(p => p.Name))
        {
            sourceWriter.WriteLine($"{function.Delegate.Name} = ({function.Delegate.Type.ToString().Remove(0, "unsafe ".Length)})LoadFunction(instance, \"{function.Name}\");");
        }
    }
}