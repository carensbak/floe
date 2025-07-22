using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public sealed class MergeBuilder : GitProcess
{
	public MergeBuilder(string mergeBranch, string targetBranch)
	{
		ArgsBuilder.AppendArgument($"{mergeBranch} {targetBranch}");
	}

	public MergeBuilder Message(string message) => AddArgument($"{Git.MergeFlags.Message} {message}");
	public MergeBuilder NoFastForward() => AddArgument(Git.MergeFlags.NoFastForward);
	public MergeBuilder Into(string branch) => AddArgument(branch);

	protected override MergeBuilder AddArgument(string arg)
	{
		ArgsBuilder.AppendArgument(arg);

		return this;
	}

	public override void Execute() => base.Execute(Git.Commands.Merge, ArgsBuilder.Build());
	public override Task ExecuteAsync() => base.ExecuteAsync(Git.Commands.Merge, ArgsBuilder.Build());
}