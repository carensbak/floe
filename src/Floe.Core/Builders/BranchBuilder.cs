using Floe.Core.Enums;
using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public sealed class BranchBuilder : GitProcess
{
	public BranchBuilder() { }
	public BranchBuilder(string branch)
	{
		ArgsBuilder.AppendArgument(branch);
	}

	public BranchBuilder Delete(string branch) => AddArgument($"{Git.BranchFlags.Delete} {branch}");
	public BranchBuilder ShowCurrent() => AddArgument(Git.BranchFlags.ShowCurrent);
	public BranchBuilder Format(BranchFormat format) => AddArgument(format.ToCommandString());

	protected override BranchBuilder AddArgument(string arg)
	{
		ArgsBuilder.AppendArgument(arg);

		return this;
	}

	public override ProcessResult Execute() => base.Execute(Git.Commands.Branch, ArgsBuilder.Build());
	public override Task<ProcessResult> ExecuteAsync() => base.ExecuteAsync(Git.Commands.Branch, ArgsBuilder.Build());

	internal static List<string> GetLocalBranches()
	{
		var result = Git.Branch()
			.Format(BranchFormat.RefnameShort)
			.Execute();

		if (!result.WasSuccess)
			throw new NotImplementedException();

		return result.StdOutLines;
	}

	internal static string GetCurrentBranch()
	{
		var result = Git.Branch()
			.ShowCurrent()
			.Execute();

		if (!result.WasSuccess)
			throw new NotImplementedException();

		return result.FirstLine;
	}
}
