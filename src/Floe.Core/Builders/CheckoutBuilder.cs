using System.Diagnostics;

using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public sealed class CheckoutBuilder : GitProcess
{
    public CheckoutBuilder(string refname)
    {
        ArgsBuilder.AppendArgument(refname);
    }

    protected override CheckoutBuilder AddArgument(string arg)
    {
        ArgsBuilder.AppendArgument(arg);

        return this;
    }

    public override Process Execute() => base.Execute(Git.Commands.Checkout, ArgsBuilder.Build());
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.Commands.Checkout, ArgsBuilder.Build());
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.Commands.Checkout, ArgsBuilder.Build());
}
