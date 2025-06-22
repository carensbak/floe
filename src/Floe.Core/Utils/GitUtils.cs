using Floe.Core.Constants;
using Floe.Core.Extensions;

namespace Floe.Core.Utils;

public static class GitUtils
{
    public static List<string> GetRepoBranches()
    {
        using var process = StdProcess.CreateStandardProcess(Git.BaseName, $"{Git.Branch} {Git.BranchFlags.FormatRefnameShort}");
        process.Start();

        var branches = process.StdOutToList();
        process.WaitForExit();

        return branches;
    }

    public static string GetCurrentBranch()
    {
        using var process = StdProcess.CreateStandardProcess(Git.BaseName, $"{Git.Branch} {Git.BranchFlags.ShowCurrent}");
        process.Start();

        var branch = process.GetStdOutFirstLine();
        process.WaitForExit();

        return branch;
    }
}
