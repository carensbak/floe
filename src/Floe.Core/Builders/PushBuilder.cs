using System.Diagnostics;

using Floe.Core.Extensions;

namespace Floe.Core.Builders;

public class PushBuilder : GitProcess
{
    public PushBuilder() { }
    public PushBuilder(string refname)
    {
        ArgsBuilder.AppendArgument(refname);
    }

    public override Process Execute() => base.Execute(Git.CommandNames.Push);
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.CommandNames.Push);
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.CommandNames.Pull);
}