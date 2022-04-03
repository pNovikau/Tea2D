using CppAst;
using Tea2D.Vulkan.Generator.CSharp;

namespace Tea2D.Vulkan.Generator
{
    public static class HandlersGenerator
    {
        public static void Generate(CppCompilation compilation, string outputFile)
        {
            using var sourceWriter = new SourceFileWriter(outputFile);

            sourceWriter.WriteLine("using System;");
            sourceWriter.WriteLine("using System.Diagnostics;");
            sourceWriter.WriteLine();

            using var _ = sourceWriter.CreateBlock("namespace Tea2D.Vulkan");

            foreach (CppTypedef cppTypedef in compilation.Typedefs)
            {
                if (cppTypedef.ElementType is not CppPointerType)
                    continue;

                if (cppTypedef.Name.StartsWith("PFN_"))
                    continue;

                var pointerHandler = new CSharpPointerHandler(cppTypedef.Name, cppTypedef);
                pointerHandler.Write(sourceWriter);

                sourceWriter.WriteLine();
            }
        }
    }
}