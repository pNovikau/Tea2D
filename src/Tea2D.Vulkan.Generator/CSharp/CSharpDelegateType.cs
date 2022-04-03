using System.Diagnostics;
using CppAst;

namespace Tea2D.Vulkan.Generator.CSharp;

[DebuggerDisplay(nameof(DebuggerDisplay))]
public class CSharpDelegateType : CSharpType
{
    public CSharpDelegateType(CppFunctionType cppFunctionType) : base(cppFunctionType)
    {
        Parameters = cppFunctionType.Parameters
            .Select(p => new CSharpType(p.Type))
            .ToArray();

        ReturnType = new CSharpType(cppFunctionType.ReturnType);
    }

    public IEnumerable<CSharpType> Parameters { get; }
    public CSharpType ReturnType { get; }

    private new string DebuggerDisplay => ToString();

    public override void Write(ISourceWriter writer)
    {
        writer.WriteLine(ToString());
    }

    public override string ToString()
    {
        var parameters = string.Join(", ", Parameters);

        return $"unsafe delegate* unmanaged<{parameters}, {ReturnType}>";
    }
}