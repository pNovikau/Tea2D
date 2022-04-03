using System;

namespace Tea2D.Core.Diagnostics.Logging
{
    public interface ILogger
    {
        void LogTrace(ReadOnlySpan<char> message);
        void LogDebug(ReadOnlySpan<char> message);
        void LogInfo(ReadOnlySpan<char> message);
        void LogWarning(ReadOnlySpan<char> message);
        void LogError(ReadOnlySpan<char> message);
        void LogFatal(ReadOnlySpan<char> message);
        void Log(in LogLevel level, ReadOnlySpan<char> message);
    }
}