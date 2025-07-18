using System.Diagnostics;

using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public class InitBuilder : GitProcess
{
    public InitBuilder(string path)
    {
        ArgsBuilder.AppendArgument(path);
    }

    public override Process Execute() => base.Execute(Git.Commands.Init, ArgsBuilder.Build());
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.Commands.Init, ArgsBuilder.Build());
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.Commands.Init, ArgsBuilder.Build());
}
