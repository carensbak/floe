using Floe.Core.Models;

namespace Floe.Core.Tests.Semver;

public class GetReleaseVersionsTests
{
    [Fact]
    public void IncludePreReleases_Should_FilterCorrectly()
    {
        var versions = new List<SemVer>()
        {
            new SemVer { Major = 1, Minor = 0, Patch = 0 },
            new SemVer { Major = 1, Minor = 1, Patch = 0 },
            new SemVer { Major = 1, Minor = 1, Patch = 0, Suffix = "alpha.1" },
            new SemVer { Major = 1, Minor = 1, Patch = 0, Suffix = "alpha.23" },
            new SemVer { Major = 1, Minor = 1, Patch = 1 },
            new SemVer { Major = 1, Minor = 1, Patch = 2 },
            new SemVer { Major = 1, Minor = 1, Patch = 3 },
            new SemVer { Major = 1, Minor = 1, Patch = 4 },
            new SemVer { Major = 1, Minor = 2, Patch = 0 },
            new SemVer { Major = 1, Minor = 2, Patch = 1 },
            new SemVer { Major = 1, Minor = 2, Patch = 2 },
            new SemVer { Major = 1, Minor = 2, Patch = 3 },
            new SemVer { Major = 1, Minor = 3, Patch = 0 },
            new SemVer { Major = 1, Minor = 4, Patch = 0, Suffix = "alpha.0" },
        };

        SemVer.GetReleaseVersions(versions).Count.ShouldBe(11); //14 versions -3 prereleases
    }
}
