using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public class LogBuilder : GitProcess
{
	public LogBuilder() { }

	protected override GitProcess AddArgument(string arg)
	{
		ArgsBuilder.AppendArgument(arg);

		return this;
	}

	public override void Execute() => base.Execute(Git.BaseName, ArgsBuilder.Build());
	public override Task ExecuteAsync() => base.ExecuteAsync(Git.BaseName, ArgsBuilder.Build());
}
