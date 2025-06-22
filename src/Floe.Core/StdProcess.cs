using System.Diagnostics;

namespace Floe.Core;

public static class StdProcess
{
    //This can become a static extension member once C# 14 releases.
    public static Process CreateStandardProcess(string cmd, string arg)
    {
        return new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = cmd,
                Arguments = arg,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            }
        };
    }

    public static Process CreateStandardProcess(string cmd, List<string> args)
    {
        var arguments = string.Join(" ", args);

        return new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = cmd,
                Arguments = arguments,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            }
        };
    }
}