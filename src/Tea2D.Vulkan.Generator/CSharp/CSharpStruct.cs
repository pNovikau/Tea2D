using System.Diagnostics;
using CppAst;

namespace Tea2D.Vulkan.Generator.CSharp;

[DebuggerDisplay(nameof(DebuggerDisplay))]
public class CSharpStruct : CSharpElement
{
    public CSharpStruct(CppClass cppClass) : base(cppClass)
    {
        Name = cppClass.Name;
        IsUnion = cppClass.ClassKind == CppClassKind.Union;
        Fields = cppClass.Fields
            .Select(p => new CSharpField(p))
            .ToArray();
    }

    public IEnumerable<CSharpField> Fields { get; }

    public string Name { get; }

    public bool IsUnion { get; }
        
    private string DebuggerDisplay => $"public partial struct {Name}";

    public override void Write(ISourceWriter writer)
    {
        var layoutKind = IsUnion
            ? "LayoutKind.Explicit"
            : "LayoutKind.Sequential";

        writer.WriteLine($"[StructLayout({layoutKind})]");

        using (writer.BeginBlock($"public partial struct {Name}"))
        {
            foreach (var field in Fields)
            {
                if (IsUnion)
                    writer.WriteLine("[FieldOffset(0)]");
                    
                field.Write(writer);
            }
        }
    }
}