using BenchmarkDotNet.Running;

namespace Tea2D.Benchmarks;

internal class Program
{
    public static void Main(string[] args)
    {
        _ = new BenchmarkSwitcher(typeof(Program).Assembly).RunAllJoined();
    }
}