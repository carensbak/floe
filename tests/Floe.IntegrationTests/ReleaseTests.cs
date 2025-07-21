using Floe.Cli.Commands;
using Floe.Core.Models;

namespace Floe.Cli.IntegrationTests;

[Collection("SequentialBranchTests")]
public class ReleaseTests(GitRepoFixture fixture)
{
    private readonly GitRepoFixture _fixture = fixture;

    [Fact]
    public void ReleasePatch_Should_IncrementPatch_And_CreateBranch()
    {
        SemVer.LatestRelease.ToString().ShouldBe("1.3.1");

        Command.Release.Patch(switchToBranch: true, pushToRemote: false);

        Git.CurrentBranch.ShouldBe("release/1.3.2");
    }

    [Fact]
    public void ReleaseMinor_Should_IncrementMinor_And_ResetPatch_And_CreateBranch()
    {
        SemVer.LatestRelease.ToString().ShouldBe("1.3.1");

        Command.Release.Minor(switchToBranch: true, pushToRemote: false);

        Git.CurrentBranch.ShouldBe("release/1.4.0");
    }

    [Fact]
    public void ReleaseMajor_Should_IncrementMajor_And_ResetMinorAndPatch_And_CreateBranch()
    {
        SemVer.LatestRelease.ToString().ShouldBe("1.3.1");

        Command.Release.Major(switchToBranch: true, pushToRemote: false);

        Git.CurrentBranch.ShouldBe("release/2.0.0");
    }
}
