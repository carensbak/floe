using System.Diagnostics;

using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public class TagBuilder : GitProcess
{
    public TagBuilder() { }

    public TagBuilder(string tag)
    {
        ArgsBuilder.AppendArgument(tag);
    }

    public TagBuilder PushTag(string tag)
    {
        Git.Push(tag)
            .ExecuteAndFinish();

        return this;
    }

    public override Process Execute() => base.Execute(Git.CommandNames.Tag);
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.CommandNames.Tag);
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.CommandNames.Tag);
}
