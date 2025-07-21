using Floe.Cli.Commands;
using Floe.Core.Exceptions;
using Floe.Core.Models;

namespace Floe.Cli.IntegrationTests;

[Collection("SequentialBranchTests")]
public class BranchTests(GitRepoFixture fixture)
{
    private readonly GitRepoFixture _fixture = fixture;

    [Fact]
    public void Create_Should_CreateBranch()
    {
        var createBranchName = "create-me";
        Command.Branch.Create(createBranchName, switchToBranch: false, pushToRemote: false);

        Git.LocalBranches.ShouldContain(createBranchName);
    }

    [Fact]
    public void DeleteCheckedOutBranch_ShouldThrow_DeleteCheckedOutBranchException()
    {
        var existingBranch = "create-me";

        if (!Git.LocalBranches.Contains(existingBranch))
            Command.Branch.Create(existingBranch, switchToBranch: false, pushToRemote: false);

        Git.Checkout(existingBranch)
            .ExecuteAndFinish();

        Git.CurrentBranch.ShouldBe(existingBranch);
        var currentBranch = existingBranch;

        Should.Throw<DeleteCheckedOutBranchException>(() => Command.Branch.Delete(currentBranch));
    }

    [Fact]
    public void Delete_Should_DeleteBranch()
    {
        var branchName = "delete-me";
        Command.Branch.Create(branchName, switchToBranch: false, pushToRemote: false);
        Git.LocalBranches.ShouldContain(branchName);

        Command.Branch.Delete(branchName, deleteAtRemote: false);
        Git.LocalBranches.ShouldNotContain(branchName);
    }
}
