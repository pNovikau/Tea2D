using System.Diagnostics;
using CppAst;
using Tea2D.Vulkan.Generator.CSharp.Utils;

namespace Tea2D.Vulkan.Generator.CSharp
{
    [DebuggerDisplay("{" + nameof(FullName) + "}")]
    public class CSharpType : CSharpElement
    {
        public CSharpType(CppType cppType) : base(cppType)
        {
        }

        public string FullName => ToString();

        public bool IsPointer => FullName.EndsWith('*');

        public override void Write(ISourceWriter writer)
        {
            writer.Write(ToString());
        }

        public override string ToString()
        {
            return CSharpTypeNameResolver.GetTypeName(GetCppElement<CppType>());
        }
    }
}