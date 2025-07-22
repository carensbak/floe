using Cocona;

using Floe.Cli.Commands;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommand("merge", (
	[Argument] string mergingBranch,
	[Option(Description = "Target branch")] string? targetBranch,
	[Option(Description = "Merge message")] string? mergeMessage,
	[Option(Description = "Delete the branch after merging? - Default = true")] bool? deleteBranch) => Command.Merge(mergingBranch, targetBranch, mergeMessage, deleteBranch));

app.AddSubCommand("release", sc =>
{
	sc.AddCommand("latest", () => Command.Release.Latest());
});

app.AddSubCommand("new", sc =>
{
	sc.AddCommand("major", () => Command.Release.Major());
	sc.AddCommand("minor", () => Command.Release.Minor());
	sc.AddCommand("patch", () => Command.Release.Patch());

	sc.AddCommand("fix", (
		[Argument] string branchSuffix,
		[Option(Description = "Switch to the created branch?")] bool? switchToBranch,
		[Option(Description = "Push branch to remote?")] bool? pushBranchToRemote) =>
	{ throw new NotImplementedException(); });

	sc.AddCommand("feature", (
		[Argument] string branchSuffix,
		[Option(Description = "Switch to the created branch?")] bool? switchToBranch,
		[Option(Description = "Push branch to remote?")] bool? pushToRemote) =>
	{ throw new NotImplementedException(); });

	sc.AddCommand("test", (
		[Argument] string branchSuffix,
		[Option(Description = "Switch to the created branch?")] bool? switchToBranch,
		[Option(Description = "Push branch to remote?")] bool? pushToRemote) =>
	{ throw new NotImplementedException(); });

	sc.AddCommand("docs", (
		[Argument] string branchSuffix,
		[Option(Description = "Switch to the created branch?")] bool? switchToBranch,
		[Option(Description = "Push branch to remote?")] bool? pushToRemote) =>
	{ throw new NotImplementedException(); });

	sc.AddCommand("tag", (
		[Argument] string tag,
		[Option(Description = "Push tag to remote?")] bool? pushToRemote) =>
	{ throw new NotImplementedException(); });

	sc.AddCommand("branch", (
		[Argument] string branch,
		[Option(Description = "Switch to the created branch?")] bool? switchToBranch,
		[Option(Description = "Push branch to remote?")] bool? pushToRemote) => Command.Branch.Create(branch, switchToBranch, pushToRemote));
});

app.AddSubCommand("rm", sc =>
{
	sc.AddCommand("branch", (
		[Argument] string branch,
		[Option(Description = "Delete branch at remote?")] bool? deleteAtRemote) => Command.Branch.Delete(branch));

	sc.AddCommand("tag", (
		[Argument] string tag,
		[Option(Description = "Delete tag at remote?")] bool? deleteAtRemote) =>
	{ throw new NotImplementedException(); });
});

await app.RunAsync();