using System;
using CppAst;

namespace Tea2D.Vulkan.Generator.CSharp
{
    public class CSharpPointerHandler : CSharpElement
    {
        public CSharpPointerHandler(string name, CppElement cppElement) : base(cppElement)
        {
            Name = name;
        }
        
        public string Name { get; }

        public override void Write(ISourceWriter writer)
        {
            const string nullValue = "IntPtr.Zero";
            
            using var _ = writer.CreateBlock($"public readonly partial struct {Name} : IEquatable<{Name}>");

            writer.WriteLine($"public readonly {nameof(IntPtr)} Handle;");
            writer.WriteLine();
            writer.WriteLine($"public {Name}({nameof(IntPtr)} existingHandle) => Handle = existingHandle;");
            writer.WriteLine();
            writer.WriteLine($"public static {Name} Null => new {Name}({nullValue});");
            writer.WriteLine();
            writer.WriteLine($"public static implicit operator {Name}({nameof(IntPtr)} handle) => new {Name}(handle);");
            writer.WriteLine();
            writer.WriteLine($"public static bool operator ==({Name} left, {Name} right) => left.Handle == right.Handle;");
            writer.WriteLine($"public static bool operator !=({Name} left, {Name} right) => left.Handle != right.Handle;");
            writer.WriteLine($"public static bool operator ==({Name} left, {nameof(IntPtr)} right) => left.Handle == right;");
            writer.WriteLine($"public static bool operator !=({Name} left, {nameof(IntPtr)} right) => left.Handle != right;");
            writer.WriteLine();
            writer.WriteLine($"public bool Equals({Name} other) => Handle == other.Handle;");
            writer.WriteLine();
            writer.WriteLine($"public override bool Equals(object obj) => obj is {Name} handle && Equals(handle);");
            writer.WriteLine();
            writer.WriteLine($"public override int GetHashCode() => Handle.GetHashCode();");
        }
    }
}