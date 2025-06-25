using Cocona;

using Floe.Core.Utils;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommand("branches", () =>
{
    var branches = GitUtils.GetRepoBranches();

    branches.ForEach(b => Console.WriteLine(b));
});

app.AddCommand("branch", () =>
{
    var branch = GitUtils.GetCurrentBranch();

    Console.WriteLine(branch);
});

await app.RunAsync();