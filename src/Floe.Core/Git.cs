using Floe.Core.Builders;
using Floe.Core.Enums;
using Floe.Core.Extensions;

namespace Floe.Core;

public static partial class Git
{
    public static string CurrentBranch => GetCurrentBranch();
    public static List<string> LocalBranches => GetLocalBranches();

    public static FetchBuilder Fetch()
    {
        return new FetchBuilder();
    }

    public static BranchBuilder Branch()
    {
        return new BranchBuilder();
    }

    public static PushBuilder Push()
    {
        return new PushBuilder();
    }

    public static PushBuilder Push(string refname)
    {
        return new PushBuilder(refname);
    }

    public static TagBuilder Tag()
    {
        return new TagBuilder();
    }

    public static TagBuilder Tag(string tag)
    {
        return new TagBuilder(tag);
    }

    public static MergeBuilder Merge(string branch)
    {
        return Merge(branch, CurrentBranch);
    }

    public static MergeBuilder Merge(string mergeBranch, string targetBranch)
    {
        return new MergeBuilder(mergeBranch, targetBranch);
    }


    private static string GetCurrentBranch()
    {
        var process = Git.Branch()
            .ShowCurrent()
            .Execute();

        var branch = process.GetStdOutFirstLine();
        process.WaitForExit();

        return branch;
    }

    private static List<string> GetLocalBranches()
    {
        var process = Git.Branch()
            .Format(BranchFormat.RefnameShort)
            .Execute();

        process.Start();

        var branches = process.StdOutToList();
        process.WaitForExitAsync();

        return branches;
    }
}
