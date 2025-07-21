using System.Diagnostics;
using System.Text;

namespace Floe.Core.Models;

public abstract class GitProcess
{
    protected StringBuilder ArgsBuilder { get; } = new();

    public abstract Process Execute();
    protected virtual Process Execute(string command, string args)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = Git.BaseName,
                Arguments = string.Join(' ', command, args),
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            }
        };

        process.Start();

        return process;
    }

    public abstract void ExecuteAndFinish();
    protected virtual void ExecuteAndFinish(string command, string args)
    {
        using var process = Execute(command, args);

        var output = process.StandardOutput.ReadToEnd();
        var errors = process.StandardError.ReadToEnd();

        process.WaitForExit();

        Console.WriteLine(output);
        Console.WriteLine(errors);
    }

    public abstract Task ExecuteAsync();
    protected virtual async Task ExecuteAsync(string command, string args)
    {
        using var process = Execute(command, args);

        var output = await process.StandardOutput.ReadToEndAsync();
        var errors = await process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        Console.WriteLine(output);
        Console.WriteLine(errors);
    }

    protected abstract GitProcess AddArgument(string arg);
}
