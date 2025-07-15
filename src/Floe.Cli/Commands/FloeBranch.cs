using Floe.Core.Models;

namespace Floe.Cli.Commands;

internal static partial class Commands
{
    public static void Branch(string branch, bool? switchToBranch = null, bool? pushToRemote = null)
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