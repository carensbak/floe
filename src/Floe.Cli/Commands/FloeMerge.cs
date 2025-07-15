using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Cli.Commands;

internal static partial class Commands
{
    public static void Merge(string mergingBranch, string? targetBranch = null, string? mergeMessage = null, bool? deleteBranch = null)
    {
        Git.Fetch()
            .ExecuteAndFinish();

        if (targetBranch.IsNullOrWhiteSpace())
            targetBranch = Git.CurrentBranch;

        if (mergeMessage.IsNullOrWhiteSpace())
            mergeMessage = $"merge: '{mergingBranch}' -> '{targetBranch}'";

        Git.Merge(mergingBranch)
            .Into(targetBranch)
            .Message(mergeMessage!)
            .NoFastForward()
            .ExecuteAndFinish();

        if (!(mergingBranch.IsFixBranch() || mergingBranch.IsReleaseBranch()) && targetBranch.IsMasterBranch())
        {
            if (deleteBranch ?? true)
                Commands.Branch.Delete(mergingBranch, deleteAtRemote: true);

            return;
        }

        if (mergingBranch.ContainsSemver())
        {
            var semver = mergingBranch.TryGetSemver();
            if (!semver.IsNullOrWhiteSpace())
            {
                Git.Tag(semver)
                    .PushTag(semver)
                    .Execute();
            }
        }

        Git.Merge(mergingBranch)
            .Into(Git.Branches.Dev)
            .NoFastForward()
            .Message($"merge: '{mergingBranch}' -> 'dev'")
            .ExecuteAndFinish();

        if (deleteBranch ?? true)
            Commands.Branch.Delete(mergingBranch, deleteAtRemote: true);

        return;
    }
}