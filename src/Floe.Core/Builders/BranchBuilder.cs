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

	public override void Execute() => base.Execute(Git.Commands.Branch, ArgsBuilder.Build());
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.Commands.Branch, ArgsBuilder.Build());
}
