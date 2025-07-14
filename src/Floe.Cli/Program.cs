using Cocona;

using Floe.Cli.Commands;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();
app.AddCommands<FloeMerge>();
app.AddCommands<FloeBranch>();

await app.RunAsync();