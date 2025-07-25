using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public sealed class InitBuilder : GitProcess
{
	public InitBuilder(string path)
	{
		ArgsBuilder.AppendArgument(path);
		ArgsBuilder.AppendArgument($"{Git.InitFlags.InitialBranch} {Git.Branches.Master}");
	}

	public InitBuilder AddGitIgnore() => CreateFile(".gitignore");
	public InitBuilder AddReadMe() => CreateFile("README.md");
	public InitBuilder AddLicense() => CreateFile("License.md");

	private InitBuilder CreateFile(string fileName)
	{
		File.Create(fileName).Dispose();

		return this;
	}

	protected override GitProcess AddArgument(string arg)
	{
		ArgsBuilder.AppendArgument(arg);

		return this;
	}

	public override ProcessResult Execute() => base.Execute(Git.Commands.Init, ArgsBuilder.Build());
	public override Task<ProcessResult> ExecuteAsync() => base.ExecuteAsync(Git.Commands.Init, ArgsBuilder.Build());
}
