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

		public static void Fix(string branchSuffix, bool? switchToBranch = null, bool? pushToRemote = null)
			=> Create($"fix/{branchSuffix}", switchToBranch ?? true, pushToRemote ?? true);

		public static void Feature(string branchSuffix, bool? switchToBranch = null, bool? pushToRemote = null)
			=> Create($"feature/{branchSuffix}", switchToBranch ?? true, pushToRemote ?? true);

		public static void Test(string branchSuffix, bool? switchToBranch = null, bool? pushToRemote = null)
			=> Create($"tests/{branchSuffix}", switchToBranch ?? true, pushToRemote ?? true);

		public static void Docs(string branchSuffix, bool? switchToBranch = null, bool? pushToRemote = null)
			=> Create($"docs/{branchSuffix}", switchToBranch ?? true, pushToRemote ?? true);
	}
}