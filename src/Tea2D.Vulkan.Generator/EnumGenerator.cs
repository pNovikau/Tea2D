using System;
using System.Collections.Generic;
using CppAst;
using Tea2D.Vulkan.Generator.CSharp;
using Tea2D.Vulkan.Generator.CSharp.Utils;

namespace Tea2D.Vulkan.Generator
{
    public static class EnumGenerator
    {
        public static void Generate(CppCompilation compilation, string outputFile)
        {
            var createdEnums = new HashSet<string>();

            using var sourceWriter = new SourceFileWriter(outputFile);

            sourceWriter.WriteLine("using System;");
            sourceWriter.WriteLine();

            using var _ = sourceWriter.CreateBlock("namespace Tea2D.Vulkan");

            foreach (var cppEnum in compilation.Enums)
            {
                var cSharpEnum = new CSharpEnum(cppEnum);
                createdEnums.Add(cSharpEnum.Name);

                cSharpEnum.Write(sourceWriter);

                sourceWriter.WriteLine();
            }
            
            IDisposable? enumBlock = null;
            foreach (var cppField in compilation.Fields)
            {
                var typeName = CSharpTypeNameResolver.GetTypeName(cppField.Type);

                if (!createdEnums.Contains(typeName))
                {
                    if (enumBlock != null)
                    {
                        enumBlock.Dispose();
                        enumBlock = null;

                        sourceWriter.WriteLine();
                    }

                    createdEnums.Add(typeName);

                    var baseType = "uint";
                    if (cppField.Type is CppQualifiedType {ElementType: CppTypedef typedef})
                        baseType = CSharpTypeNameResolver.GetTypeName(typedef.ElementType);
                    else if (cppField.Type is CppQualifiedType cppQualifiedType)
                        baseType = CSharpTypeNameResolver.GetTypeName(cppQualifiedType.ElementType);

                    if (typeName.EndsWith("FlagBits2")) 
                        typeName = typeName.Replace("FlagBits2", "Flags2");

                    sourceWriter.WriteLine("[Flags]");
                    enumBlock = sourceWriter.CreateBlock($"public enum {typeName} : {baseType}");
                }

                sourceWriter.WriteLine($"{CSharpNamingHelper.NormalizeEnumItemName(cppField.Name)} = {cppField.InitValue},");
            }

            enumBlock?.Dispose();
        }
    }
}