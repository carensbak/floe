using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Cli.Commands;

internal static partial class Commands
{
    public static void Merge(string mergingBranch, string? targetBranch = null, string? mergeMessage = null)
    {
        Git.Fetch()
            .ExecuteAndFinish();

        if (targetBranch.IsNullOrWhiteSpace())
            targetBranch = Git.CurrentBranch;

        if (mergeMessage.IsNullOrWhiteSpace())
            mergeMessage = $"merge: '{mergingBranch}' -> '{targetBranch}'";

        Git.Merge(mergingBranch)
            .Message(mergeMessage!)
            .NoFastForward()
            .ExecuteAndFinish();

        if (!(mergingBranch.IsFixBranch() || mergingBranch.IsReleaseBranch()) && targetBranch.IsMasterBranch())
            return;

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
            .Into(Git.BranchNames.Dev)
            .NoFastForward()
            .Message($"merge: '{mergingBranch}' -> 'dev'")
            .ExecuteAndFinish();
    }
}