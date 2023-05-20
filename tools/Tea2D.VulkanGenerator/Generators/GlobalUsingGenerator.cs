using CppAst;

namespace Tea2D.VulkanGenerator.Generators;

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
        }
    }
}