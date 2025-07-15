using Floe.Core.Models;

namespace Floe.Cli.Commands;

internal static partial class Commands
{
    public void Branch(
        [Argument] string branch,
        [Option(Description = "Switch to the created branch?")] bool? switchToBranch,
        [Option(Description = "Push branch to remote?")] bool? pushToRemote)
    {
        Git.Branch(branch)
            .ExecuteAndFinish();

        if (pushToRemote ?? true)
        {
            Git.Push()
                .SetOriginUpstream(branch)
                .ExecuteAndFinish();
        }

        if (switchToBranch ?? true)
        {
            Git.Checkout(branch)
                .ExecuteAndFinish();
        }
    }
}