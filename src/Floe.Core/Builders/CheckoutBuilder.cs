using System.Diagnostics;

using Floe.Core.Extensions;

namespace Floe.Core.Builders;

public class CheckoutBuilder : GitProcess
{
    public CheckoutBuilder(string refname)
    {
        ArgsBuilder.AppendArgument(refname);
    }

    public override Process Execute() => base.Execute(Git.CommandNames.Checkout);
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.CommandNames.Checkout);
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.CommandNames.Checkout);
}
