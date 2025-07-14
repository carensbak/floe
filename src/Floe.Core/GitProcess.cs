using System.Diagnostics;
using System.Text;

namespace Floe.Core;

public abstract class GitProcess : IGitProcess
{
    protected StringBuilder ArgsBuilder { get; } = new();

    public abstract Process Execute();
    protected Process Execute(string command)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = Git.BaseName,
                Arguments = string.Join(' ', command, ArgsBuilder.ToString()),
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            }
        };

        process.Start();

        return process;
    }

    public abstract void ExecuteAndFinish();
    protected void ExecuteAndFinish(string command)
    {
        using var process = Execute(command);

        process.WaitForExit();
    }

    public abstract Task ExecuteAsync();
    protected async Task ExecuteAsync(string command)
    {
        using var process = Execute(command);

        await process.WaitForExitAsync();
    }
}
