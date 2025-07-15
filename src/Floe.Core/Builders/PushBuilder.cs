using System.Diagnostics;

using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public class PushBuilder : GitProcess
{
    public PushBuilder() { }
    public PushBuilder(string refname)
    {
        ArgsBuilder.AppendArgument(refname);
    }

    public PushBuilder DeleteRef(string refName)
    {
        ArgsBuilder.AppendFormat($"{Git.PushFlags.DeleteRef} {refName}");

        return this;
    }

    public PushBuilder Origin()
    {
        ArgsBuilder.AppendArgument(Git.Origin);

        return this;
    }

    public PushBuilder SetOriginUpstream(string upstream)
    {
        SetUpstream($"{Git.Origin} {upstream}");

        return this;
    }

    public PushBuilder SetUpstream(string upstream)
    {
        ArgsBuilder.AppendArgument($"{Git.PushFlags.SetUpstream} {upstream}");

        return this;
    }

    public override Process Execute() => base.Execute(Git.CommandNames.Push);
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.CommandNames.Push);
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.CommandNames.Push);
}