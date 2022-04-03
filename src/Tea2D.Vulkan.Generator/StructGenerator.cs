using CppAst;
using Tea2D.Vulkan.Generator.CSharp;

namespace Tea2D.Vulkan.Generator;

public static class StructGenerator
{
    public static void Generate(CppCompilation compilation, string outputFile)
    {
        using var sourceWriter = new SourceFileWriter(outputFile);

        sourceWriter.WriteLine("using System.Runtime.InteropServices;");
        sourceWriter.WriteLine();

        sourceWriter.WriteLine("namespace Tea2D.Vulkan;");
        sourceWriter.WriteLine();

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