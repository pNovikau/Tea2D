using CppAst;

namespace Tea2D.Vulkan.Generator.CSharp;

public abstract class CSharpElement
{
    public CppElement CppElement { get; }

    protected CSharpElement(CppElement cppElement)
    {
        CppElement = cppElement;
    }

    public abstract void Write(ISourceWriter writer);

    protected TCppElement GetCppElement<TCppElement>() where TCppElement : CppElement
        => (TCppElement) CppElement;
}