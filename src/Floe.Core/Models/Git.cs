using Floe.Core.Builders;
using Floe.Core.Extensions;

namespace Floe.Core.Models;

public static partial class Git
{
	public static string CurrentBranch => BranchBuilder.GetCurrentBranch();
	public static List<string> LocalBranches => BranchBuilder.GetLocalBranches();
	public static List<string> Tags => TagBuilder.GetAllTags();

	public static AddBuilder Add() => new AddBuilder();

	public static BranchBuilder Branch() => new BranchBuilder();
	public static BranchBuilder Branch(string branch) => new BranchBuilder(branch);

	public static CheckoutBuilder Checkout(string refname) => new CheckoutBuilder(refname);

	public static CommitBuilder Commit() => new CommitBuilder();

	public static InitBuilder Init() => Init(".");
	public static InitBuilder Init(string path) => new InitBuilder(path);

	public static LogBuilder Log() => new LogBuilder();

	public static FetchBuilder Fetch() => new FetchBuilder();

	public static PushBuilder Push() => new PushBuilder();
	public static PushBuilder Push(string refname) => new PushBuilder(refname);

	public static TagBuilder Tag() => new TagBuilder();
	public static TagBuilder Tag(string tag) => new TagBuilder(tag);
	public static void Tag(params string[] tags) => tags.ForEach(t => Tag(t).Execute());

	public static MergeBuilder Merge(string branch) => Merge(branch, CurrentBranch);
	public static MergeBuilder Merge(string mergeBranch, string targetBranch) => new MergeBuilder(mergeBranch, targetBranch);
}
