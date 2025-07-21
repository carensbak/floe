using Floe.Core.Constants;

namespace Floe.Core.Logging;

public static class Logger
{
    public static void LogInfo(string message) => LogToConsole(message, ConsoleColor.Blue, Diagnostics.InfoIcon);
    public static void LogSuccess(string message) => LogToConsole(message, ConsoleColor.Green, Diagnostics.SuccessSymbol);
    public static void LogWarning(string message) => LogToConsole(message, ConsoleColor.Yellow, Diagnostics.WarningSymbol);
    public static void LogError(string message) => LogToConsole(message, ConsoleColor.Red, Diagnostics.ErrorSymbol);

    private static void LogToConsole(string message, ConsoleColor fgColor, string diagnosticsSymbol)
    {
        Console.ForegroundColor = fgColor;
        Console.WriteLine($"{diagnosticsSymbol}{message}");
    }
}