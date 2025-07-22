using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Cli.Commands;

internal static partial class Command
{
    internal static void Merge(string mergingBranch, string? targetBranch = null, string? mergeMessage = null, bool? deleteBranch = null)
    {
        Git.Fetch()
            .Execute();

        if (targetBranch.IsNullOrWhiteSpace())
            targetBranch = Git.CurrentBranch;

        if (mergeMessage.IsNullOrWhiteSpace())
            mergeMessage = $"merge: '{mergingBranch}' -> '{targetBranch}'";

        Git.Merge(mergingBranch)
            .Into(targetBranch)
            .Message(mergeMessage!)
            .NoFastForward()
            .Execute();

        if (!(mergingBranch.IsFixBranch() || mergingBranch.IsReleaseBranch()) && targetBranch.IsMasterBranch())
        {
            if (deleteBranch ?? true)
                Command.Branch.Delete(mergingBranch, deleteAtRemote: true);

            return;
        }

        if (mergingBranch.ContainsSemver())
        {
            var semver = mergingBranch.TryGetSemver();
            if (!semver.IsNullOrWhiteSpace())
            {
				//tagging and pushing here.
            }
        }

        Git.Merge(mergingBranch)
            .Into(Git.Branches.Dev)
            .NoFastForward()
            .Message($"merge: '{mergingBranch}' -> 'dev'")
            .Execute();

        if (deleteBranch ?? true)
            Command.Branch.Delete(mergingBranch, deleteAtRemote: true);

        return;
    }
}