using System.Diagnostics;
using System.Text;

namespace Floe.Core.Models;

public abstract class GitProcess : IGitProcess
{
    protected StringBuilder ArgsBuilder { get; } = new();

    public abstract Process Execute();
    protected Process Execute(string command, string args)
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
    protected void ExecuteAndFinish(string command, string args)
    {
        using var process = Execute(command, args);

        var output = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        Console.WriteLine(output);
    }

    public abstract Task ExecuteAsync();
    protected async Task ExecuteAsync(string command, string args)
    {
        using var process = Execute(command, args);

        var output = await process.StandardOutput.ReadToEndAsync();

        await process.WaitForExitAsync();

        Console.WriteLine(output);
    }
}
