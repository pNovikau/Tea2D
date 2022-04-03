using CppAst;
using Tea2D.Vulkan.Generator.CSharp.Utils;

namespace Tea2D.Vulkan.Generator.CSharp;

public class CSharpFunctionParameter : CSharpElement
{
    public CSharpFunctionParameter(CppParameter cppElement) : base(cppElement)
    {
        Name = CSharpNamingHelper.NormalizeFiledName(cppElement.Name);
        Type = new CSharpType(cppElement.Type);
        IsByRef = cppElement.Type is CppPointerType;
        IsConst = cppElement.Type is CppPointerType { ElementType: CppQualifiedType { Qualifier: CppTypeQualifier.Const } };
    }

    public string Name { get; }
    public bool IsConst { get; }
    public bool IsByRef { get; }
    public CSharpType Type { get; }

    public override void Write(ISourceWriter writer) => writer.Write(ToString());

    public override string ToString()
    {
        return $"{Type} {Name}";
    }
}