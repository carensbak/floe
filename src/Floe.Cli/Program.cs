using Cocona;

using Floe.Cli.Commands;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommand("merge", (
	[Argument] string mergingBranch,
	[Option(Description = "Target branch")] string? targetBranch,
	[Option(Description = "Merge message")] string? mergeMessage,
	[Option(Description = "Delete the branch after merging? - Default = true")] bool? deleteBranch) => Command.Merge(mergingBranch, targetBranch, mergeMessage, deleteBranch));

app.AddSubCommand("branch", sc =>
{
	sc.AddCommand("create", (
		[Argument] string branch,
		[Option(Description = "Switch to the created branch?")] bool? switchToBranch,
		[Option(Description = "Push branch to remote?")] bool? pushToRemote) => Command.Branch.Create(branch, switchToBranch, pushToRemote));

	sc.AddCommand("delete", (
		[Argument] string branch,
		[Option(Description = "Delete branch at remote?")] bool? deleteAtRemote) => Command.Branch.Delete(branch));
});

app.AddSubCommand("release", sc =>
{
	sc.AddCommand("major", () => Command.Release.Major());
	sc.AddCommand("minor", () => Command.Release.Minor());
	sc.AddCommand("patch", () => Command.Release.Patch());
	sc.AddCommand("latest", () => Command.Release.Latest());
});

await app.RunAsync();