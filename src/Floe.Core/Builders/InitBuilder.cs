using System.Diagnostics;

using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public sealed class InitBuilder : GitProcess
{
    public InitBuilder(string path)
    {
        ArgsBuilder.AppendArgument(path);
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

    public override Process Execute() => base.Execute(Git.Commands.Init, ArgsBuilder.Build());
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.Commands.Init, ArgsBuilder.Build());
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.Commands.Init, ArgsBuilder.Build());
}
