using System.Diagnostics;

using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public sealed class AddBuilder : GitProcess
{
    public AddBuilder() { }

    public AddBuilder File(string fileNamePattern) => AddArgument(fileNamePattern);
    public AddBuilder AllFiles() => AddArgument(".");

    protected override AddBuilder AddArgument(string arg)
    {
        ArgsBuilder.AppendArgument(arg);

        return this;
    }

    public override Process Execute() => base.Execute(Git.Commands.Add, ArgsBuilder.Build());
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.Commands.Add, ArgsBuilder.Build());
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.Commands.Add, ArgsBuilder.Build());
}
