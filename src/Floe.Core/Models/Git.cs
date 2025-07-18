using Floe.Core.Builders;
using Floe.Core.Enums;
using Floe.Core.Extensions;
using Floe.Core.Logging;

namespace Floe.Core.Models;

public static partial class Git
{
    public static string CurrentBranch => GetCurrentBranch();
    public static List<string> LocalBranches => GetLocalBranches();
    public static List<string> Tags => GetAllTags();

    public static BranchBuilder Branch() => new BranchBuilder();
    public static BranchBuilder Branch(string branch) => new BranchBuilder(branch);

    public static CheckoutBuilder Checkout(string refname) => new CheckoutBuilder(refname);

    public static FetchBuilder Fetch() => new FetchBuilder();

    public static PushBuilder Push() => new PushBuilder();
    public static PushBuilder Push(string refname) => new PushBuilder(refname);

    public static TagBuilder Tag() => new TagBuilder();
    public static TagBuilder Tag(string tag) => new TagBuilder(tag);

    public static MergeBuilder Merge(string branch) => Merge(branch, CurrentBranch);
    public static MergeBuilder Merge(string mergeBranch, string targetBranch) => new MergeBuilder(mergeBranch, targetBranch);


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

    private static List<string> GetAllTags()
    {
        Logger.LogInfo("Fetching all tags...");
        Git.Fetch()
            .Tags()
            .ExecuteAndFinish();

        Logger.LogInfo("Fetched all tags");

        var process = Git.Tag()
            .Execute();

        var tags = process.StdOutToList();
        process.WaitForExitAsync();

        return tags;
    }
}
