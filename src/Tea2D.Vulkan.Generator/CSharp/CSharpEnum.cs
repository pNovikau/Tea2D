using System.Collections.Generic;
using System.Linq;
using CppAst;

namespace Tea2D.Vulkan.Generator.CSharp
{
    public class CSharpEnum : CSharpElement
    {
        private CSharpEnumItem[]? _items;

        public CSharpEnum(CppEnum cppEnum) : base(cppEnum)
        {
        }

        public string Name => GetCppElement<CppEnum>().Name;

        public IEnumerable<CSharpEnumItem> Items
        {
            get
            {
                if (_items != null)
                    return _items;

                _items = GetCppElement<CppEnum>().Items
                    .Select(p => new CSharpEnumItem(p))
                    .ToArray();

                return _items;
            }
        }

        public override void Write(ISourceWriter writer)
        {
            var isBitmask =
                Name.EndsWith("FlagBits2") ||
                Name.EndsWith("FlagBits") ||
                Name.EndsWith("FlagBitsEXT") ||
                Name.EndsWith("FlagBitsKHR") ||
                Name.EndsWith("FlagBitsNV") ||
                Name.EndsWith("FlagBitsAMD") ||
                Name.EndsWith("FlagBitsMVK") ||
                Name.EndsWith("FlagBitsNN");

            if (isBitmask)
            { 
                writer.WriteLine("[Flags]");
            }

            using (writer.CreateBlock($"internal enum {Name}"))
            {
                foreach (var enumItem in Items)
                {
                    // cause an compile error 
                    if (((CppEnumItem) enumItem.CppElement).Name == "VK_STRUCTURE_TYPE_SURFACE_CAPABILITIES_2_EXT")
                        continue;

                    if (isBitmask)
                    {
                        writer.WriteLine();
                        writer.WriteLine("/// <summary>");
                        writer.WriteLine($"/// Hex value 0x{enumItem.Value:x8}");
                        writer.WriteLine("/// <summary>");
                    }
                    
                    enumItem.Write(writer);
                }
            }
        }
    }
}