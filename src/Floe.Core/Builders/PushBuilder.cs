using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public sealed class PushBuilder : GitProcess
{
	public PushBuilder() { }
	public PushBuilder(string refname)
	{
		ArgsBuilder.AppendArgument(refname);
	}

	public PushBuilder DeleteRef(string refName) => AddArgument($"{Git.PushFlags.DeleteRef} {Git.Origin} {refName}");
	public PushBuilder Origin() => AddArgument(Git.Origin);
	public PushBuilder SetOriginUpstream(string upstream) => SetUpstream($"{Git.Origin} {upstream}");
	public PushBuilder SetUpstream(string upstream) => AddArgument($"{Git.PushFlags.SetUpstream} {upstream}");

	protected override PushBuilder AddArgument(string arg)
	{
		ArgsBuilder.AppendArgument(arg);

		return this;
	}

	public override void Execute() => base.Execute(Git.Commands.Push, ArgsBuilder.Build());
	public override Task ExecuteAsync() => base.ExecuteAsync(Git.Commands.Push, ArgsBuilder.Build());
}