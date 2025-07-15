using System.Diagnostics;

using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public class FetchBuilder : GitProcess
{
    public FetchBuilder Branch(string branch)
    {
        ArgsBuilder.AppendArgument(branch);

        return this;
    }

    public FetchBuilder Tags()
    {
        ArgsBuilder.AppendArgument(Git.FetchFlags.Tags);

        return this;
    }

    public override Process Execute() => base.Execute(Git.Commands.Fetch, ArgsBuilder.Build());
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.Commands.Fetch, ArgsBuilder.Build());
    public override async Task ExecuteAsync() => await base.ExecuteAsync(Git.Commands.Fetch, ArgsBuilder.Build());
}
