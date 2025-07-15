using Cocona;

using Floe.Core.Extensions;

using Git = Floe.Core.Git;

namespace Floe.Cli.Commands;

public sealed class FloeMerge
{
    public void Merge([Argument] string mergingBranch, [Option(Description = "Merge message")] string? mergeMessage)
    {
        Git.Fetch()
            .ExecuteAndFinish();

        var currentBranch = Git.CurrentBranch;

        if (mergeMessage.IsNullOrWhiteSpace())
            mergeMessage = $"merge: '{mergingBranch}' -> '{currentBranch}'";

        Git.Merge(mergingBranch)
            .Message(mergeMessage!)
            .NoFastForward()
            .ExecuteAndFinish();

        if (!(mergingBranch.IsFixBranch() || mergingBranch.IsReleaseBranch()) && currentBranch.IsMasterBranch())
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