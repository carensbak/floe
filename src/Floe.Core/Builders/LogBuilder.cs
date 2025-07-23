using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public class LogBuilder : GitProcess
{
	public LogBuilder() { }

	public LogBuilder RevParse(string branchName) => AddArgument($"{Git.LogFlags.RevParse} {branchName}");
	public LogBuilder MergeBase(string branchOne, string branchTwo) => AddArgument($"{Git.LogFlags.MergeBase} {branchOne} {branchTwo}");

	protected override LogBuilder AddArgument(string arg)
	{
		ArgsBuilder.AppendArgument(arg);

		return this;
	}

	public override ProcessResult Execute() => base.Execute("", ArgsBuilder.Build());
	public override Task<ProcessResult> ExecuteAsync() => base.ExecuteAsync("", ArgsBuilder.Build());

	public string GetBranchOffCommit(string branchOne, string branchTwo)
	{
		var result = Git.Log()
			.MergeBase(branchOne, branchTwo)
			.Execute();

		if (!result.WasSuccess)
			throw new NotImplementedException();

		return result.FirstLine;
	}

	public string GetLatestCommitOnBranch(string branchName)
	{
		var result = Git.Log()
			.RevParse(branchName)
			.Execute();

		if (!result.WasSuccess)
			throw new NotImplementedException();

		return result.FirstLine;
	}
}
