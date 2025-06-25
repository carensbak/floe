using Floe.Core.Constants;
using Floe.Core.Extensions;

namespace Floe.Core.Utils;

public static class GitUtils
{
    public static void FetchAllRemotes()
    {
        using var process = StdProcess.CreateStandardProcess(Git.BaseName, $"{Git.Fetch} {Git.FetchFlags.All}");
        process.Start();

        process.WaitForExit();
    }

    public static async Task<List<string>> GetRepoBranchesAsync()
    {
        using var process = StdProcess.CreateStandardProcess(Git.BaseName, $"{Git.Branch} {Git.BranchFlags.FormatRefnameShort}");
        process.Start();

        var branches = process.StdOutToList();
        await process.WaitForExitAsync();

        return branches;
    }

    public static async Task<string> GetCurrentBranchAsync()
    {
        using var process = StdProcess.CreateStandardProcess(Git.BaseName, $"{Git.Branch} {Git.BranchFlags.ShowCurrent}");
        process.Start();

        var branch = process.GetStdOutFirstLine();
        await process.WaitForExitAsync();

        return branch;
    }

    public static async Task<bool> CreateBranch(string branchName, bool? pushToRemote)
    {
        using var createBranchProcess = StdProcess.CreateStandardProcess(Git.BaseName, [Git.Branch, branchName]);
        createBranchProcess.Start();
        await createBranchProcess.WaitForExitAsync();

        if (createBranchProcess.ExitCode != 0)
            return false;

        if (pushToRemote ?? true)
        {
            using var pushProcess = StdProcess.CreateStandardProcess(Git.BaseName, [
                Git.Push, Git.Origin,
                Git.PushFlags.SetUpstream, branchName]);
            pushProcess.Start();
            await pushProcess.WaitForExitAsync();

            if (pushProcess.ExitCode != 0)
                return false;
        }

        return true;
    }

    public static async Task<bool> CreateTag(string tag, bool pushToRemote)
    {
        using var createTagProcess = StdProcess.CreateStandardProcess(Git.BaseName, [Git.Tag, tag]);
        createTagProcess.Start();
        await createTagProcess.WaitForExitAsync();

        if (createTagProcess.ExitCode != 0)
            return false;

        if (pushToRemote)
        {
            using var pushProcess = StdProcess.CreateStandardProcess(Git.BaseName, [Git.Push, Git.Origin, tag]);
            pushProcess.Start();
            await pushProcess.WaitForExitAsync();

            if (pushProcess.ExitCode != 0)
                return false;
        }

        return true;
    }
}
