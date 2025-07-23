using Floe.Cli.Commands;
using Floe.Core.Models;

namespace Floe.Cli.IntegrationTests;

[Collection("SequentialBranchTests")]
public class NewTests
{
	[Fact]
	public void NewFix_Should_BranchFromMaster()
	{
		//ensure we're not standing on master when testing, to verify that master gets checked out before branchiong.
		if (Git.CurrentBranch is Git.Branches.Master)
			Git.Checkout(Git.Branches.Dev)
				.Execute();

		Git.CurrentBranch.ShouldNotBe(Git.Branches.Master);

		var branchName = "my-fix";
		Command.Branch.Fix(branchName, switchToBranch: true, pushToRemote: true);

		var fixBranchName = $"{Git.Branches.Fix}/{branchName}";
		Git.CurrentBranch.ShouldBe(fixBranchName);

		//Verify that fix branch was branched off from master.
		var branchOffCommit = Git.Log()
			.GetBranchOffCommit(fixBranchName, Git.Branches.Master);

		var latestFixCommit = Git.Log()
			.GetLatestCommitOnBranch(fixBranchName);

		branchOffCommit.ShouldBe(latestFixCommit);
	}

	[Fact]
	public void NewFeature_Should_BranchFromDev()
	{
		if (Git.CurrentBranch is Git.Branches.Dev)
			Git.Checkout(Git.Branches.Master)
				.Execute();

		Git.CurrentBranch.ShouldNotBe(Git.Branches.Dev);

		var branchName = "my-feature";
		Command.Branch.Feature(branchName, switchToBranch: true, pushToRemote: false);

		var featureBranchName = $"{Git.Branches.Feature}/{branchName}";
		Git.CurrentBranch.ShouldBe(featureBranchName);

		var branchOffCommit = Git.Log()
			.GetBranchOffCommit(featureBranchName, Git.Branches.Dev);

		var latestFeatureCommit = Git.Log()
			.GetLatestCommitOnBranch(featureBranchName);

		branchOffCommit.ShouldBe(latestFeatureCommit);
	}

	[Fact]
	public void NewTest_Should_BranchFromDev()
	{
		if (Git.CurrentBranch is Git.Branches.Dev)
			Git.Checkout(Git.Branches.Master)
				.Execute();

		Git.CurrentBranch.ShouldNotBe(Git.Branches.Dev);

		var branchName = "my-tests";
		Command.Branch.Test(branchName, switchToBranch: true, pushToRemote: false);

		var testBranchName = $"{Git.Branches.Tests}/{branchName}";
		Git.CurrentBranch.ShouldBe(testBranchName);

		var branchOffCommit = Git.Log()
			.GetBranchOffCommit(testBranchName, Git.Branches.Dev);

		var latestTestCommit = Git.Log()
			.GetLatestCommitOnBranch(testBranchName);

		branchOffCommit.ShouldBe(latestTestCommit);
	}

	[Fact]
	public void NewTest_Can_BranchFromFeature()
	{
		var featureBranchName = "foo";
		var fullFeatureBranchName = $"{Git.Branches.Feature}/{featureBranchName}";
		Command.Branch.Feature(featureBranchName, switchToBranch: true, pushToRemote: false);
		Git.CurrentBranch.ShouldBe(fullFeatureBranchName);

		var testBranchName = "bar";
		Command.Branch.Test(testBranchName, switchToBranch: true, pushToRemote: false);
		var fullTestBranchName = $"{Git.Branches.Tests}/{testBranchName}";
		Git.CurrentBranch.ShouldBe(fullTestBranchName);

		var branchOffCommit = Git.Log()
			.GetBranchOffCommit(fullTestBranchName, fullFeatureBranchName);

		var latestTestCommit = Git.Log()
			.GetLatestCommitOnBranch(fullTestBranchName);

		branchOffCommit.ShouldBe(latestTestCommit);
	}

	[Fact]
	public void NewDocs_Should_BranchFromDev()
	{
		if (Git.CurrentBranch is Git.Branches.Dev)
			Git.Checkout(Git.Branches.Master)
				.Execute();

		Git.CurrentBranch.ShouldNotBe(Git.Branches.Dev);

		var branchName = "my-docs";
		Command.Branch.Docs(branchName, switchToBranch: true, pushToRemote: false);

		var docsBranchName = $"{Git.Branches.Docs}/{branchName}";
		Git.CurrentBranch.ShouldBe(docsBranchName);

		var branchOffCommit = Git.Log()
			.GetBranchOffCommit(docsBranchName, Git.Branches.Dev);

		var latestDocsCommit = Git.Log()
			.GetLatestCommitOnBranch(docsBranchName);

		branchOffCommit.ShouldBe(latestDocsCommit);
	}

	[Fact]
	public void NewDocs_Can_BranchFromFeature()
	{
		var featureBranchName = "baz";
		var fullFeatureBranchName = $"{Git.Branches.Feature}/{featureBranchName}";
		Command.Branch.Feature(featureBranchName, switchToBranch: true, pushToRemote: false);
		Git.CurrentBranch.ShouldBe(fullFeatureBranchName);

		var docsBranchName = "buzz";
		Command.Branch.Docs(docsBranchName, switchToBranch: true, pushToRemote: false);
		var fullDocsBranchName = $"{Git.Branches.Docs}/{docsBranchName}";
		Git.CurrentBranch.ShouldBe(fullDocsBranchName);

		var branchOffCommit = Git.Log()
			.GetBranchOffCommit(fullDocsBranchName, fullFeatureBranchName);

		var latestDocsCommit = Git.Log()
			.GetLatestCommitOnBranch(fullDocsBranchName);

		branchOffCommit.ShouldBe(latestDocsCommit);
	}
}
