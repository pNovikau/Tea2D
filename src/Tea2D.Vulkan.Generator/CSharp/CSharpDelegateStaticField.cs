using System.Diagnostics;
using CppAst;

namespace Tea2D.Vulkan.Generator.CSharp;

[DebuggerDisplay(nameof(DebuggerDisplay))]
public class CSharpDelegateStaticField : CSharpElement
{
    public CSharpDelegateStaticField(CppFunction cppFunction) : base(cppFunction)
    {
        var cppFunctionType = new CppFunctionType(cppFunction.ReturnType);

        foreach (var parameter in cppFunction.Parameters)
        {
            cppFunctionType.Parameters.Add(new CppParameter(parameter.Type, parameter.Name));
        }

        Type = new CSharpDelegateType(cppFunctionType);
        ReturnType = new CSharpType(cppFunction.ReturnType);
        Name = $"__{cppFunction.Name}";
    }

    public string Name { get; }
    public CSharpDelegateType Type { get; }
    public CSharpType ReturnType { get; }

    private new string DebuggerDisplay => ToString();

    public override void Write(ISourceWriter writer)
    {
        writer.WriteLine(ToString());
    }

    public override string ToString()
    {
        return $"private static {Type} {Name};";
    }
}