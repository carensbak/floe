using System.Diagnostics;

using Floe.Core.Extensions;

namespace Floe.Core.Builders;

public class MergeBuilder : GitProcess
{
    public MergeBuilder(string mergeBranch, string targetBranch)
    {
        ArgsBuilder.AppendArgument($"{mergeBranch} {targetBranch}");
    }

    public MergeBuilder Message(string message)
    {
        ArgsBuilder.AppendArgument($"{Git.MergeFlags.Message} {message}");

        return this;
    }

    public MergeBuilder NoFastForward()
    {
        ArgsBuilder.AppendArgument(Git.MergeFlags.NoFastForward);

        return this;
    }

    public MergeBuilder Into(string branch)
    {
        ArgsBuilder.AppendArgument(branch);

        return this;
    }

    public override Process Execute() => base.Execute(Git.CommandNames.Merge);
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.CommandNames.Merge);
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.CommandNames.Merge);
}