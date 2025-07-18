using Floe.Core.Constants;

namespace Floe.Core.Logging;

public static class Logger
{
    public static void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{Diagnostics.InfoIcon}{message}");
    }

    public static void LogSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{Diagnostics.SuccessSymbol}{message}");
    }

    public static void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{Diagnostics.WarningSymbol}{message}");
    }

    public static void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{Diagnostics.ErrorSymbol}{message}");
    }
}