using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Tea2D.Core.Diagnostics.Logging
{
    public class Logger : ILogger
    {
        private static readonly object Lock = new();

        public static readonly ILogger Instance = new Logger();

        public void LogTrace(ReadOnlySpan<char> message) => Log(LogLevel.Trace, message);

        public void LogDebug(ReadOnlySpan<char> message) => Log(LogLevel.Debug, message);

        public void LogInfo(ReadOnlySpan<char> message) => Log(LogLevel.Info, message);

        public void LogWarning(ReadOnlySpan<char> message) => Log(LogLevel.Warning, message);

        public void LogError(ReadOnlySpan<char> message) => Log(LogLevel.Error, message);

        public void LogFatal(ReadOnlySpan<char> message) => Log(LogLevel.Fatal, message);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Log(in LogLevel level, ReadOnlySpan<char> message)
        {
            Debug.Assert(!Monitor.IsEntered(Lock));

            lock (Lock)
            {
                Console.Write(LogLevelToString(level));
                Console.Out.WriteLine(message);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string LogLevelToString(in LogLevel level)
        {
// disable missing default section warning 
#pragma warning disable 8509
            return level switch
#pragma warning restore 8509
            {
                LogLevel.Trace => "[Trace]   ",
                LogLevel.Debug => "[Debug]   ",
                LogLevel.Info => "[Info]    ",
                LogLevel.Warning => "[Warning] ",
                LogLevel.Error => "[Error]   ",
                LogLevel.Fatal => "[Fatal]   "
            };
        }
    }
}