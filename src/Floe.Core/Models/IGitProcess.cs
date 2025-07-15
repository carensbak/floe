using System.Diagnostics;

namespace Floe.Core.Models;

public interface IGitProcess
{
    Process Execute();
    void ExecuteAndFinish();
    Task ExecuteAsync();
}
