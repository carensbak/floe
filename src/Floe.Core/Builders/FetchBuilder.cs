using System.Diagnostics;

using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public sealed class FetchBuilder : GitProcess
{
    public FetchBuilder() { }

    public FetchBuilder Branch(string branch) => AddArgument(branch);
    public FetchBuilder Tags() => AddArgument(Git.FetchFlags.Tags);

    protected override FetchBuilder AddArgument(string arg)
    {
        ArgsBuilder.AppendArgument(arg);

        return this;
    }

    public override Process Execute() => base.Execute(Git.Commands.Fetch, ArgsBuilder.Build());
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.Commands.Fetch, ArgsBuilder.Build());
    public override async Task ExecuteAsync() => await base.ExecuteAsync(Git.Commands.Fetch, ArgsBuilder.Build());
}
