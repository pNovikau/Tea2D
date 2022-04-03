using System.Diagnostics;
using CppAst;
using Tea2D.Vulkan.Generator.CSharp.Utils;

namespace Tea2D.Vulkan.Generator.CSharp;

public class CSharpField : CSharpElement
{
    private readonly CppField _cppField;

    public CSharpField(CppField cppField) : base(cppField)
    {
        _cppField = cppField;
        Type = CSharpElementFactory.CreateType(cppField.Type);
    }

    public CSharpElementVisibility Visibility
    {
        get
        {
            return _cppField.Visibility switch
            {
                CppVisibility.Default => CSharpElementVisibility.Private,
                CppVisibility.Public => CSharpElementVisibility.Public,
                CppVisibility.Protected => CSharpElementVisibility.Protected,
                CppVisibility.Private => CSharpElementVisibility.Private,
                _ => CSharpElementVisibility.Private
            };
        }
    }

    public string Name => CSharpNamingHelper.NormalizeFiledName(_cppField.Name);
    public CSharpType Type { get; }

    public bool IsCanBeFixed =>
        Type.CppElement is CppArrayType {ElementType: CppPrimitiveType} ||
        Type.CppElement is CppArrayType {ElementType: CppTypedef {ElementType: CppPrimitiveType}};

    private string VisibilityString => Visibility.ToString().ToLower();

    public override void Write(ISourceWriter writer)
    {
        if (Type is CSharpArrayType arrayType)
        {
            if (IsCanBeFixed)
            {
                writer.WriteLine($"{VisibilityString} {CSharpNamingHelper.UnsafeKeyword} {CSharpNamingHelper.FixedKeyword} {Type} {Name}[{arrayType.Size}];");
                return;
            }

            for (var i = 0; i < arrayType.Size; i++)
            {
                if (Type.IsPointer)
                    writer.WriteLine($"{VisibilityString} {CSharpNamingHelper.UnsafeKeyword} {Type} {Name}_{i};");
                else
                    writer.WriteLine($"{VisibilityString} {Type} {Name}_{i};");
            }

            return;
        }

        if (Type is CSharpDelegateType)
        {
            writer.WriteLine($"{VisibilityString} {Type} {Name};");
            return;
        }

        switch (_cppField.Type.ToString())
        {
            case "ANativeWindow*":
            case "CAMetalLayer*":
            case "const CAMetalLayer*":
                writer.WriteLine($"{VisibilityString} IntPtr {Name};");
                return;
        }

        if (Type.IsPointer)
            writer.WriteLine($"{VisibilityString} {CSharpNamingHelper.UnsafeKeyword} {Type} {Name};");
        else
            writer.WriteLine($"{VisibilityString} {Type} {Name};");
    }
}