using Cocona;

using Floe.Core;

namespace Floe.Cli.Commands;

public class FloeBranch
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