using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public sealed class CheckoutBuilder : GitProcess
{
	public CheckoutBuilder(string refname)
	{
		ArgsBuilder.AppendArgument(refname);
	}

	protected override CheckoutBuilder AddArgument(string arg)
	{
		ArgsBuilder.AppendArgument(arg);

		return this;
	}

	public override ProcessResult Execute() => base.Execute(Git.Commands.Checkout, ArgsBuilder.Build());
	public override Task<ProcessResult> ExecuteAsync() => base.ExecuteAsync(Git.Commands.Checkout, ArgsBuilder.Build());
}
