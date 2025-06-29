using Cocona;

using Floe.Cli.Commands;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();
app.AddCommands<FloeMerge>();

await app.RunAsync();