using CppAst;

namespace Tea2D.Vulkan.Generator.CSharp.Utils
{
    public static class CSharpElementFactory
    {
        public static CSharpType CreateType(CppType cppType)
        {
            if (cppType is CppTypedef {ElementType: CppPointerType {ElementType: CppFunctionType functionType}})
                return new CSharpDelegate(functionType);

            if (cppType is CppArrayType cppArrayType)
                return new CSharpArrayType(cppArrayType);
            
            return new CSharpType(cppType);
        }
    }
}