using CommandLine;
using CommandLine.Text;
using CppAst;
using Tea2D.VulkanGenerator.Options;

namespace Tea2D.VulkanGenerator;

internal class Program
{
    public static int Main(string[] args)
    {
        var argumentParser = new Parser();

        var result = argumentParser.ParseArguments<RunOptions>(args);

        var exitCode = result.MapResult(
            runOptions => ProcessRunAsync(runOptions).GetAwaiter().GetResult(),
            _ => ProcessErrors(result));

        return exitCode;
    }

    private static int ProcessErrors(ParserResult<RunOptions> result)
    {
        var helpText = HelpText.AutoBuild(result, h =>
        {
            h.AdditionalNewLineAfterOption = false;
            return HelpText.DefaultParsingErrorsHandler(result, h);
        }, e => e);

        Console.WriteLine(helpText);

        return Constants.ErrorExitCode;
    }

    private static async Task<int> ProcessRunAsync(RunOptions runOptions)
    {
        Logger.Initialize(runOptions.Verbosity);

        if (Directory.Exists(runOptions.HeadersRepositoryPath) is false)
        {
            Logger.LogFatal($"{runOptions.HeadersRepositoryPath} directory not found");

            return Constants.ErrorExitCode;
        }

        if (Directory.Exists(runOptions.OutputPath) is false)
        {
            Logger.LogFatal($"{runOptions.HeadersRepositoryPath} directory not found");

            return Constants.ErrorExitCode;
        }

        var cppParserOptions = new CppParserOptions
        {
            ParseMacros = true,
        };

        if (runOptions.CLangIncludePath is not null)
        {
            if (Directory.Exists(runOptions.CLangIncludePath) is false)
            {
                Logger.LogFatal($"{runOptions.CLangIncludePath} directory not found");

                return Constants.ErrorExitCode;
            }

            cppParserOptions.SystemIncludeFolders.Add(runOptions.CLangIncludePath);
        }
        
        var includeDirectory = Path.Combine(runOptions.HeadersRepositoryPath, "include");
        cppParserOptions.IncludeFolders.Add(includeDirectory);

        var vulkanHeaderPath = Path.Combine(runOptions.HeadersRepositoryPath, "include" , "vulkan", "vulkan.h");
        var compilation = CppParser.ParseFile(vulkanHeaderPath, cppParserOptions);

        compilation.Diagnostics.Log();

        if (compilation.HasErrors)
        {
            Logger.LogFatal("Couldn't parse header file");
            return Constants.ErrorExitCode;
        }

        return Constants.SuccessExitCode;
    }
}