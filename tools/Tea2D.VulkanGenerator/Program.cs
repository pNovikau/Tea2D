using CppAst;
using Tea2D.VulkanGenerator.Generators;

var headerFile = Environment.GetEnvironmentVariable("VULKAN_HEADER_PATH");

if (File.Exists(headerFile) is false)
{
    Logger.LogFatal("`VULKAN_HEADER_PATH` environment variable is not set or file not found");
    return;
}

var compilation = CppParser.ParseFile(headerFile);

foreach (var diagnosticMessage in compilation.Diagnostics.Messages)
{
    if (diagnosticMessage.Type == CppLogMessageType.Info)
        Logger.LogInfo($"{diagnosticMessage.Location}: {diagnosticMessage.Text}");
    
    else if (diagnosticMessage.Type == CppLogMessageType.Warning)
        Logger.LogWarning($"{diagnosticMessage.Location}: {diagnosticMessage.Text}");
    
    else if (diagnosticMessage.Type == CppLogMessageType.Error)
        Logger.LogError($"{diagnosticMessage.Location}: {diagnosticMessage.Text}");
}

if (compilation.HasErrors)
{
    Logger.LogFatal("Couldn't parse header file");
    return;
}

// GlobalUsingGenerator.Generate(compilation, );