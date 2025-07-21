using Floe.Core.Models;

namespace Floe.Cli.Commands;

internal static partial class Command
{
    internal static class Branch
    {
        public static void Create(string branch, bool? switchToBranch = null, bool? pushToRemote = null)
        {
            Git.Branch(branch)
                .ExecuteAndFinish();

            if (pushToRemote ?? true)
            {
                Git.Push()
                    .SetOriginUpstream(branch)
                    .ExecuteAndFinish();
            }

            if (switchToBranch ?? true)
            {
                Git.Checkout(branch)
                    .ExecuteAndFinish();
            }
        }

        public static void Delete(string branch, bool? deleteAtRemote = null)
        {
            Git.Branch()
                .Delete(branch)
                .ExecuteAndFinish();

            if (deleteAtRemote ?? true)
            {
                Git.Push()
                    .DeleteRef(branch)
                    .ExecuteAndFinish();
            }
        }
    }
}