using System.Diagnostics;
using CppAst;
using Tea2D.Vulkan.Generator.CSharp.Utils;

namespace Tea2D.Vulkan.Generator.CSharp
{
    [DebuggerDisplay("{" + nameof(FullName) + "}")]
    public class CSharpArrayType : CSharpType
    {
        public CSharpArrayType(CppArrayType cppArrayType) : base(cppArrayType)
        {
        }

        public int Size => GetCppElement<CppArrayType>().Size;

        public override string ToString()
        {
            return CSharpTypeNameResolver.GetTypeName(GetCppElement<CppArrayType>().ElementType);
        }
    }
}