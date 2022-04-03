using System.Collections.Generic;
using CppAst;

namespace Tea2D.Vulkan.Generator.CSharp.Utils
{
    public static class CSharpTypeNameResolver
    {
        private static readonly Dictionary<string, string> TypeNameMapping = new()
        {
            ["uint8_t"] = "byte",
            ["uint16_t"] = "ushort",
            ["uint32_t"] = "uint",
            ["uint64_t"] = "ulong",
            ["int8_t"] = "sbyte",
            ["int32_t"] = "int",
            ["int16_t"] = "short",
            ["int64_t"] = "long",
            ["int64_t*"] = "long*",
            ["char"] = "byte",
            ["size_t"] = "uint",
            ["DWORD"] = "uint",

            // typedef uint32_t VkBool32;
            ["VkBool32"] = "uint",

            // typedef uint32_t VkFlags;
            ["VkFlags"] = "uint",

            // typedef uint64_t VkDeviceSize;
            ["VkDeviceSize"] = "ulong",

            // typedef uint32_t VkSampleMask;
            ["VkSampleMask"] = "uint",

            //typedef uint64_t VkDeviceAddress;
            ["VkDeviceAddress"] = "ulong",
            
            //typedef uint64_t VkFlags64;
            ["VkFlags64"] = "ulong",
            
            //typedef VkAccelerationStructureTypeKHR VkAccelerationStructureTypeNV;
            ["VkAccelerationStructureTypeNV"] = "VkAccelerationStructureTypeKHR",
        };

        public static string GetTypeName(CppType cppType, bool isPointer = false)
        {
            if (cppType is CppPrimitiveType cppPrimitiveType)
                return GetPrimitiveTypeName(cppPrimitiveType, isPointer);

            if (cppType is CppTypedef cppTypedef)
                return GetTypeName(cppTypedef.Name, isPointer);

            if (cppType is CppEnum cppEnum)
                return GetTypeName(cppEnum.Name, isPointer);

            if (cppType is CppQualifiedType cppQualifiedType)
                return GetTypeName(cppQualifiedType.ElementType, isPointer);

            if (cppType is CppClass cppClass)
                return GetTypeName(cppClass.Name, isPointer);

            if (cppType is CppPointerType cppPointerType)
                return GetPointerTypeName(cppPointerType);

            if (cppType is CppArrayType cppArrayType)
                return GetTypeName(cppArrayType.ElementType, true);

            return string.Empty;
        }

        private static string GetTypeName(string name, bool isPointer)
        {
            var resultName = TypeNameMapping.TryGetValue(name, out var cSharpName)
                ? cSharpName
                : name;

            return isPointer ? resultName + "*" : resultName;
        }

        private static string GetPointerTypeName(CppPointerType cppPointerType)
        {
            if (cppPointerType.ElementType is not CppQualifiedType cppQualifiedType)
                return GetTypeName(cppPointerType.ElementType, true);

            if (cppQualifiedType.ElementType is CppPointerType subPointerType)
                return GetTypeName(subPointerType, true) + '*';

            return GetTypeName(cppQualifiedType.ElementType, true);
        }

        private static string GetPrimitiveTypeName(CppPrimitiveType cppPrimitiveType, bool isPointer)
        {
            switch (cppPrimitiveType.Kind)
            {
                case CppPrimitiveKind.Void:
                    return isPointer ? "void*" : "void";
                case CppPrimitiveKind.Bool:
                    return isPointer ? "bool*" : "bool";
                case CppPrimitiveKind.Char:
                    return isPointer ? "byte*" : "byte";
                case CppPrimitiveKind.Short:
                    return isPointer ? "short*" : "short";
                case CppPrimitiveKind.Int:
                    return isPointer ? "int*" : "int";
                case CppPrimitiveKind.UnsignedShort:
                    return isPointer ? "ushort*" : "ushort";
                case CppPrimitiveKind.UnsignedInt:
                    return isPointer ? "uint*" : "uint";
                case CppPrimitiveKind.Float:
                    return isPointer ? "float*" : "float";
                case CppPrimitiveKind.Double:
                    return isPointer ? "double*" : "double";

                case CppPrimitiveKind.WChar:
                case CppPrimitiveKind.LongLong:
                case CppPrimitiveKind.UnsignedChar:
                case CppPrimitiveKind.UnsignedLongLong:
                case CppPrimitiveKind.LongDouble:
                default:
                    return string.Empty;
            }
        }
    }
}