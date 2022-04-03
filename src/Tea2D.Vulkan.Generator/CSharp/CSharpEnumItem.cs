using System.Diagnostics;
using CppAst;
using Tea2D.Vulkan.Generator.CSharp.Utils;

namespace Tea2D.Vulkan.Generator.CSharp
{
    [DebuggerDisplay("{Name}:{Value}")]
    public class CSharpEnumItem : CSharpElement
    {
        public CSharpEnumItem(CppEnumItem cppEnumItem) : base(cppEnumItem)
        {
        }

        public string Name => CSharpNamingHelper.NormalizeEnumItemName(GetCppElement<CppEnumItem>().Name);
        public long Value => GetCppElement<CppEnumItem>().Value;

        public override void Write(ISourceWriter writer)
        {
            writer.WriteLine($"{Name} = {Value},");
        }
    }
}