using Cocona;

using Floe.Core;
using Floe.Core.Constants;
using Floe.Core.Extensions;
using Floe.Core.Utils;

namespace Floe.Cli.Commands;

internal static class FloeMerge
{
    public static async Task Merge([Argument] string mergingBranch, [Option(Description = "Merge message")] string? mergeMessage)
    {
        GitUtils.FetchAllRemotes();

        var currentBranch = await GitUtils.GetCurrentBranchAsync();
        var branches = await GitUtils.GetRepoBranchesAsync();

        if (!branches.Contains(mergingBranch))
        {
            await Console.Error.WriteLineAsync($"{Diagnostics.ErrorSymbol} '{mergingBranch}' is not a valid branch.");
            Environment.Exit(1);
        }

        if (!string.IsNullOrWhiteSpace(mergeMessage))
            mergeMessage = $"merge: '{mergingBranch}' -> '{currentBranch}'";

        using var mergeProcess = StdProcess.CreateStandardProcess(Git.BaseName, [
            Git.Merge, mergingBranch,
            Git.MergeFlags.NoFastForward,
            Git.MergeFlags.Message, mergeMessage]);
        mergeProcess.Start();

        var errors = await mergeProcess.StandardError.ReadToEndAsync();

        await mergeProcess.WaitForExitAsync();
        if (mergeProcess.ExitCode != 0)
        {
            await Console.Error.WriteLineAsync(errors);
            Environment.Exit(mergeProcess.ExitCode);
        }

        if ((mergingBranch.IsFixBranch() || mergingBranch.IsReleaseBranch()) && currentBranch.IsMasterBranch())
        {
            if (mergingBranch.ContainsSemver())
            {
                var semver = mergingBranch.TryGetSemver();
                if (!string.IsNullOrWhiteSpace(semver))
                    await GitUtils.CreateTag(semver, pushToRemote: true);
            }

            var devMergeMessage = $"merge: '{mergingBranch}' -> 'dev'";

            using var devMergeProcess = StdProcess.CreateStandardProcess(Git.BaseName, [
                Git.Merge, mergingBranch, Git.DefaultBranchNames.Dev,
                Git.MergeFlags.NoFastForward,
                Git.MergeFlags.Message, devMergeMessage]);
            devMergeProcess.Start();
            await devMergeProcess.WaitForExitAsync();
        }
    }
}