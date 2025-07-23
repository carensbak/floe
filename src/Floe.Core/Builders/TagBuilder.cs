using Floe.Core.Extensions;
using Floe.Core.Logging;
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

	public override ProcessResult Execute() => base.Execute(Git.Commands.Tag, ArgsBuilder.Build());
	public override Task<ProcessResult> ExecuteAsync() => base.ExecuteAsync(Git.Commands.Tag, ArgsBuilder.Build());

	internal static List<string> GetAllTags()
	{
		Logger.LogInfo("Fetching all tags...");
		Git.Fetch()
			.Tags()
			.Execute();

		Logger.LogInfo("Fetched all tags");

		var result = Git.Tag()
			.Execute();

		if (!result.WasSuccess)
			throw new NotImplementedException();

		return result.StdOutLines;
	}
}
