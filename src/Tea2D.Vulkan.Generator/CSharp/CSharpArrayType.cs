using System.Diagnostics;
using CppAst;
using Tea2D.Vulkan.Generator.CSharp.Utils;

namespace Tea2D.Vulkan.Generator.CSharp;

[DebuggerDisplay(nameof(DebuggerDisplay))]
public class CSharpArrayType : CSharpType
{
    public CSharpArrayType(CppArrayType cppArrayType) : base(cppArrayType)
    {
    }

    public int Size => GetCppElement<CppArrayType>().Size;
        
    private new string DebuggerDisplay => ToString();

    public override string ToString()
    {
        return CSharpTypeNameResolver.GetTypeName(GetCppElement<CppArrayType>().ElementType);
    }
}