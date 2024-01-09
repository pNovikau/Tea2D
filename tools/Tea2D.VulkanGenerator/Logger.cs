using System.Globalization;

namespace Tea2D.VulkanGenerator;

internal static class Logger
{
    private static readonly object Locker = new();

    private static LogLevel _logLevel = LogLevel.Info;

    public static void Initialize(string logLevel)
    {
        if (Enum.TryParse<LogLevel>(logLevel, out var level))
            _logLevel = level;
    }

    public static void LogTrace(ReadOnlySpan<char> message) => Log(LogLevel.Trace, message);

    public static void LogDebug(ReadOnlySpan<char> message) => Log(LogLevel.Debug, message);

    public static void LogInfo(ReadOnlySpan<char> message) => Log(LogLevel.Info, message);

    public static void LogWarning(ReadOnlySpan<char> message) => Log(LogLevel.Warning, message);

    public static void LogError(ReadOnlySpan<char> message) => Log(LogLevel.Error, message);

    public static void LogFatal(ReadOnlySpan<char> message) => Log(LogLevel.Fatal, message);

    private static void Log(LogLevel level, ReadOnlySpan<char> message)
    {
        if (level < _logLevel)
            return;

        lock (Locker)
            LogInternal(level, message);
    }
    
    private static void LogInternal(LogLevel level, ReadOnlySpan<char> message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Out.Write(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Console.Out.Write(' ');

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(LogLevelToString(level));

        Console.ForegroundColor = GetForegroundColor(level);
        Console.Out.WriteLine(message);
    }

    private static ConsoleColor GetForegroundColor(LogLevel level)
    {
        return level switch
        {
            LogLevel.Trace => ConsoleColor.DarkGreen,
            LogLevel.Debug => ConsoleColor.DarkGreen,
            LogLevel.Info => ConsoleColor.White,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.DarkRed,
            LogLevel.Fatal => ConsoleColor.DarkRed,
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
        };
    }

    private static string LogLevelToString(LogLevel level)
    {
        return level switch
        {
            LogLevel.Trace => "[Trace]   ",
            LogLevel.Debug => "[Debug]   ",
            LogLevel.Info => "[Info]    ",
            LogLevel.Warning => "[Warning] ",
            LogLevel.Error => "[Error]   ",
            LogLevel.Fatal => "[Fatal]   ",
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
        };
    }

    private enum LogLevel : byte
    {
        Trace,
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }
}