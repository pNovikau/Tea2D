using System.Diagnostics;
using CppAst;
using Tea2D.Vulkan.Generator.CSharp.Utils;

namespace Tea2D.Vulkan.Generator.CSharp;

[DebuggerDisplay(nameof(DebuggerDisplay))]
public class CSharpType : CSharpElement
{
    private readonly string _typeName;
        
    public CSharpType(CppType cppType) : base(cppType)
    {
        _typeName = CSharpTypeNameResolver.GetTypeName(GetCppElement<CppType>());
    }

    public bool IsPointer => _typeName.EndsWith('*');
    public bool IsVoid => _typeName.Equals("void", StringComparison.OrdinalIgnoreCase);

    protected string DebuggerDisplay => _typeName;

    public override void Write(ISourceWriter writer)
    {
        writer.Write(_typeName);
    }

    public override string ToString() => _typeName;
}