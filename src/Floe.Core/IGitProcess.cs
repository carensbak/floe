using System.Diagnostics;

namespace Floe.Core;

public interface IGitProcess
{
    Process Execute();
    void ExecuteAndFinish();
    Task ExecuteAsync();
}
