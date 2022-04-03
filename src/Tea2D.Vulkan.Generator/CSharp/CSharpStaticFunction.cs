using CppAst;

namespace Tea2D.Vulkan.Generator.CSharp;

public class CSharpStaticFunction : CSharpElement
{
    public CSharpStaticFunction(CppFunction cppElement) : base(cppElement)
    {
        Parameters = cppElement.Parameters
            .Select(p => new CSharpFunctionParameter(p))
            .ToArray();

        ReturnType = new CSharpType(cppElement.ReturnType);
        Name = cppElement.Name;
        Delegate = new CSharpDelegateStaticField(cppElement);
    }

    public string Name { get; }
    public IEnumerable<CSharpFunctionParameter> Parameters { get; }
    public CSharpType ReturnType { get; }
    public CSharpDelegateStaticField Delegate { get; }

    public override void Write(ISourceWriter writer)
    {
        var parameters = string.Join(", ", Parameters);
        var parametersNames = string.Join(", ", Parameters.Select(p => p.Name));

        Delegate.Write(writer);

        using var _ = writer.BeginBlock($"internal static {ReturnType} {Name}({parameters})");
            
        if (!ReturnType.IsVoid)
            writer.WriteLine($"return {Delegate.Name}({parametersNames});");
        else
            writer.WriteLine($"{Delegate.Name}({parametersNames});");
    }
}