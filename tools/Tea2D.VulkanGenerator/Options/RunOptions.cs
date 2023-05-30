using CommandLine;

namespace Tea2D.VulkanGenerator.Options;

public class RunOptions
{
    [Option('r', "repository", Required = true, HelpText = "Vulkan-Headers repository path")]
    public string HeadersRepositoryPath { get; set; } = string.Empty;

    [Option('o', "output", Required = true, HelpText = "Output directory path")]
    public string OutputPath { get; set; } = string.Empty;

    [Option('c', "clang", Required = false, HelpText = "CLang include directory path")]
    public string? CLangIncludePath { get; set; }
    
    [Option("verbosity", Required = false, HelpText = "Sets the verbosity level")]
    public string Verbosity { get; set; }
}