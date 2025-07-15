using System.Diagnostics;

using Floe.Core.Enums;
using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public class BranchBuilder : GitProcess
{
    public BranchBuilder() { }

    public BranchBuilder(string branch)
    {
        ArgsBuilder.AppendArgument(branch);
    }

    public BranchBuilder ShowCurrent()
    {
        ArgsBuilder.AppendArgument(Git.BranchFlags.ShowCurrent);

        return this;
    }

    public BranchBuilder Format(BranchFormat format)
    {
        ArgsBuilder.AppendArgument(format.ToCommandString());

        return this;
    }

    public override Process Execute() => base.Execute(Git.CommandNames.Branch);
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.CommandNames.Branch);
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.CommandNames.Branch);
}
