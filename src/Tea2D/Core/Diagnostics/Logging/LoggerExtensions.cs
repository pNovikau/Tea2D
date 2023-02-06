using System;

namespace Tea2D.Core.Diagnostics.Logging;

public static class LoggerExtensions
{
    [System.Diagnostics.Conditional("DEBUG")]
    public static void Trace(this ILogger logger, ReadOnlySpan<char> message)
    {
        logger.LogTrace(message);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void Debug(this ILogger logger, ReadOnlySpan<char> message)
    {
        logger.LogDebug(message);
    }
}