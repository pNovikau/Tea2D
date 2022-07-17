namespace Tea2D.Vulkan.Generator;

public interface ISourceWriter : IDisposable
{
    IDisposable BeginBlock(string prefix);
    void Write(char symbol);
    void Write(string str);
    void WriteLine(char symbol);
    void WriteLine(string str);
    void WriteLine();
}