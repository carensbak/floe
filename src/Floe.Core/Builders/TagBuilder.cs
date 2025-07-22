using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public sealed class TagBuilder : GitProcess
{
    public TagBuilder() { }
    public TagBuilder(string tag)
    {
        ArgsBuilder.AppendArgument(tag);
    }

    protected override TagBuilder AddArgument(string arg)
    {
        ArgsBuilder.AppendArgument(arg);

        return this;
    }

	public override void Execute() => base.Execute(Git.Commands.Tag, ArgsBuilder.Build());
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.Commands.Tag, ArgsBuilder.Build());
}
