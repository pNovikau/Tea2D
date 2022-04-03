using System;
using CppAst;
using Tea2D.Vulkan.Generator.CSharp;

namespace Tea2D.Vulkan.Generator
{
    public static class StructGenerator
    {
        public static void Generate(CppCompilation compilation, string outputFile)
        {
            using var sourceWriter = new SourceFileWriter(outputFile);

            sourceWriter.WriteLine("using System;");
            sourceWriter.WriteLine("using System.Runtime.InteropServices;");
            sourceWriter.WriteLine();

            foreach (var cppTypedef in compilation.Typedefs)
            {
                if (cppTypedef.ElementType is CppPointerType)
                    continue;

                if (cppTypedef.Name.StartsWith("PFN_")
                    || cppTypedef.Name.Equals("VkBool32", StringComparison.OrdinalIgnoreCase)
                    || cppTypedef.Name.Equals("VkFlags", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (cppTypedef.Name.EndsWith("Flags", StringComparison.OrdinalIgnoreCase) ||
                    cppTypedef.Name.EndsWith("FlagsKHR", StringComparison.OrdinalIgnoreCase) ||
                    cppTypedef.Name.EndsWith("FlagsEXT", StringComparison.OrdinalIgnoreCase) ||
                    cppTypedef.Name.EndsWith("FlagsNV", StringComparison.OrdinalIgnoreCase) ||
                    cppTypedef.Name.EndsWith("FlagsAMD", StringComparison.OrdinalIgnoreCase) ||
                    cppTypedef.Name.EndsWith("FlagsMVK", StringComparison.OrdinalIgnoreCase) ||
                    cppTypedef.Name.EndsWith("FlagsNN", StringComparison.OrdinalIgnoreCase))
                {
                    //typedef VkFlags VkBuildAccelerationStructureFlagsKHR;
                    //typedef VkBuildAccelerationStructureFlagsKHR VkBuildAccelerationStructureFlagsNV;
                    if (cppTypedef.ElementType.GetDisplayName().Equals("VkFlags", StringComparison.OrdinalIgnoreCase) ||
                        cppTypedef.ElementType.GetDisplayName().Equals("VkBuildAccelerationStructureFlagsKHR", StringComparison.OrdinalIgnoreCase))
                    {
                        sourceWriter.WriteLine($"using {cppTypedef.Name} = System.UInt32;");
                    }
                }
            }

            sourceWriter.WriteLine();
            
            using var _ = sourceWriter.CreateBlock("namespace Tea2D.Vulkan");

            foreach (var cppClass in compilation.Classes)
            {
                if (cppClass.ClassKind == CppClassKind.Class ||
                    cppClass.SizeOf == 0 ||
                    cppClass.Name.EndsWith("_T"))
                {
                    continue;
                }

                var cSharpStruct = new CSharpStruct(cppClass);
                cSharpStruct.Write(sourceWriter);

                sourceWriter.WriteLine();
            }
        }
    }
}