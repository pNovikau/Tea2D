using System.Globalization;

internal static class Logger
{
    public static void LogTrace(ReadOnlySpan<char> message) => Log(LogLevel.Trace, message);

    public static void LogDebug(ReadOnlySpan<char> message) => Log(LogLevel.Debug, message);

    public static void LogInfo(ReadOnlySpan<char> message) => Log(LogLevel.Info, message);

    public static void LogWarning(ReadOnlySpan<char> message) => Log(LogLevel.Warning, message);

    public static void LogError(ReadOnlySpan<char> message) => Log(LogLevel.Error, message);

    public static void LogFatal(ReadOnlySpan<char> message) => Log(LogLevel.Fatal, message);

    private static void Log(in LogLevel level, ReadOnlySpan<char> message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Out.Write(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Console.Out.Write(' ');

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(LogLevelToString(in level));

        Console.ForegroundColor = GetForegroundColor(in level);
        Console.Out.WriteLine(message);
    }

    private static ConsoleColor GetForegroundColor(in LogLevel level)
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
    
    private static string LogLevelToString(in LogLevel level)
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