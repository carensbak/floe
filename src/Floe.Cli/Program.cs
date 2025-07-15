using Cocona;

using Floe.Cli.Commands;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommand("merge", (
    [Argument] string mergingBranch,
    [Option(Description = "Target branch")] string? targetBranch,
    [Option(Description = "Merge message")] string? mergeMessage) => Commands.Merge(mergingBranch, targetBranch, mergeMessage));

app.AddSubCommand("branch", sc =>
{
    sc.AddCommand("create", (
        [Argument] string branch,
        [Option(Description = "Switch to the created branch?")] bool? switchToBranch,
        [Option(Description = "Push branch to remote?")] bool? pushToRemote) => Commands.Branch.Create(branch, switchToBranch, pushToRemote));

    sc.AddCommand("delete", (
        [Argument] string branch,
        [Option(Description = "Delete branch at remote?")] bool? deleteAtRemote) => Commands.Branch.Delete(branch));
});

app.AddSubCommand("release", sc =>
{
    sc.AddCommand("major", () => Commands.Release.Major());
    sc.AddCommand("minor", () => Commands.Release.Minor());
    sc.AddCommand("patch", () => Commands.Release.Patch());
    sc.AddCommand("latest", () => Commands.Release.Latest());
});

await app.RunAsync();