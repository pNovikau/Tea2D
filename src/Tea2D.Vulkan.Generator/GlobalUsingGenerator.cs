using CppAst;
using Tea2D.Vulkan.Generator.CSharp.Utils;

namespace Tea2D.Vulkan.Generator;

public static class GlobalUsingGenerator
{
    private static readonly HashSet<string> PrimitivesTypesName = new()
    {
        "uint8_t",
        "uint16_t",
        "uint32_t",
        "uint64_t",
        "int8_t",
        "int32_t",
        "int16_t",
        "int64_t",
        "char",
        "size_t",
        "DWORD"
    };

    public static void Generate(CppCompilation compilation, string outputFile)
    {
        using var sourceWriter = new SourceFileWriter(outputFile);

        sourceWriter.WriteLine("global using global::System;");
        sourceWriter.WriteLine();

        var typeDefs = new Dictionary<string, string>();

        foreach (var cppTypedef in compilation.Typedefs)
        {
            if (cppTypedef.ElementType is CppPointerType)
                continue;
                
            if (cppTypedef.Name.StartsWith("PFN_"))
            {
                continue;
            }

            if (typeDefs.TryGetValue(cppTypedef.ElementType.GetDisplayName(), out var type))
            {
                sourceWriter.WriteLine($"global using {cppTypedef.Name} = {type};");

                typeDefs.Add(cppTypedef.Name, type);
            }
            else
            {
                var globalType = PrimitivesTypesName.Contains(cppTypedef.ElementType.GetDisplayName())
                    ? $"global::{CSharpTypeNameResolver.GetFullTypeName(cppTypedef.ElementType)}"
                    : $"global::Tea2D.Vulkan.{CSharpTypeNameResolver.GetFullTypeName(cppTypedef.ElementType)}";

                sourceWriter.WriteLine($"global using {cppTypedef.Name} = {globalType};");
                typeDefs.Add(cppTypedef.Name, globalType);
            }
        }
    }
}