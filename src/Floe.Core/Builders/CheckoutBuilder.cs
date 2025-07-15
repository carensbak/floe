using System.Diagnostics;

using Floe.Core.Extensions;
using Floe.Core.Models;

namespace Floe.Core.Builders;

public class CheckoutBuilder : GitProcess
{
    public CheckoutBuilder(string refname)
    {
        ArgsBuilder.AppendArgument(refname);
    }

    public override Process Execute() => base.Execute(Git.Commands.Checkout);
    public override void ExecuteAndFinish() => base.ExecuteAndFinish(Git.Commands.Checkout);
    public override Task ExecuteAsync() => base.ExecuteAsync(Git.Commands.Checkout);
}
