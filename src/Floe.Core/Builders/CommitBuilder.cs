using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public sealed class CommitBuilder : GitProcess
{
	public CommitBuilder() { }

	public CommitBuilder WithMessage(string message) => AddArgument($"{Git.CommitFlags.Message} \"{message}\"");
	public CommitBuilder AllowEmpty() => AddArgument(Git.CommitFlags.AllowEmpty);

	protected override CommitBuilder AddArgument(string arg)
	{
		ArgsBuilder.AppendArgument(arg);

		return this;
	}

	public override ProcessResult Execute() => base.Execute(Git.Commands.Commit, ArgsBuilder.Build());
	public override Task<ProcessResult> ExecuteAsync() => base.ExecuteAsync(Git.Commands.Commit, ArgsBuilder.Build());
}
