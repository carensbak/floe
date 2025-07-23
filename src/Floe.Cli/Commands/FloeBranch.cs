using Floe.Core.Exceptions;
using Floe.Core.Extensions;
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
		{
			if (Git.CurrentBranch != Git.Branches.Master)
			{
				Git.Checkout(Git.Branches.Master)
					.Execute();
			}

			Create($"{Git.Branches.Fix}/{branchSuffix}", switchToBranch ?? true, pushToRemote ?? true);
		}

		public static void Feature(string branchSuffix, bool? switchToBranch = null, bool? pushToRemote = null)
		{
			if (Git.CurrentBranch != Git.Branches.Dev)
			{
				Git.Checkout(Git.Branches.Dev)
					.Execute();
			}

			Create($"{Git.Branches.Feature}/{branchSuffix}", switchToBranch ?? true, pushToRemote ?? true);
		}

		public static void Test(string branchSuffix, bool? switchToBranch = null, bool? pushToRemote = null)
		{
			if (!Git.CurrentBranch.IsFeatureBranch() || Git.CurrentBranch != Git.Branches.Dev)
			{
				Git.Checkout(Git.Branches.Dev)
					.Execute();
			}

			Create($"{Git.Branches.Tests}/{branchSuffix}", switchToBranch ?? true, pushToRemote ?? true);
		}

		public static void Docs(string branchSuffix, bool? switchToBranch = null, bool? pushToRemote = null)
		{
			if (!Git.CurrentBranch.IsFeatureBranch() || Git.CurrentBranch != Git.Branches.Dev)
			{
				Git.Checkout(Git.Branches.Dev)
					.Execute();
			}

			Create($"{Git.Branches.Docs}/{branchSuffix}", switchToBranch ?? true, pushToRemote ?? true);
		}
	}
}