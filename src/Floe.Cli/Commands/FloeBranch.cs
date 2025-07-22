using Floe.Core.Exceptions;
using Floe.Core.Models;

namespace Floe.Cli.Commands;

internal static partial class Command
{
	internal static class Branch
	{
		public static void Create(string branch, bool? switchToBranch = null, bool? pushToRemote = null)
		{
			Git.Branch(branch)
				.Execute();

			if (pushToRemote ?? true)
			{
				Git.Push()
					.SetOriginUpstream(branch)
					.Execute();
			}

			if (switchToBranch ?? true)
			{
				Git.Checkout(branch)
					.Execute();
			}
		}

		public static void Delete(string branch, bool? deleteAtRemote = null)
		{
			if (branch == Git.CurrentBranch)
				throw new DeleteCheckedOutBranchException(branch);

			Git.Branch()
				.Delete(branch)
				.Execute();

			if (deleteAtRemote ?? true)
			{
				Git.Push()
					.DeleteRef(branch)
					.Execute();
			}
		}
	}
}