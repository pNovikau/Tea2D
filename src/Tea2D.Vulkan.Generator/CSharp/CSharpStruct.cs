using System.Collections.Generic;
using System.Linq;
using CppAst;

namespace Tea2D.Vulkan.Generator.CSharp
{
    public class CSharpStruct : CSharpElement
    {
        private CSharpField[]? _fields;

        public CSharpStruct(CppClass cppClass) : base(cppClass)
        {
        }

        public IEnumerable<CSharpField> Fields
        {
            get
            {
                if (_fields != null)
                    return _fields;

                _fields = GetCppElement<CppClass>().Fields
                    .Select(p => new CSharpField(p))
                    .ToArray();

                return _fields;
            }
        }

        public string Name => GetCppElement<CppClass>().Name;

        public bool IsUnion => GetCppElement<CppClass>().ClassKind == CppClassKind.Union;

        public override void Write(ISourceWriter writer)
        {
            var layoutKind = IsUnion
                ? "LayoutKind.Explicit"
                : "LayoutKind.Sequential";

            writer.WriteLine($"[StructLayout({layoutKind})]");

            using (writer.CreateBlock($"internal partial struct {Name}"))
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
}