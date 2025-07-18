using Floe.Core.Models;

namespace Floe.Core.Tests.Semver;

public class FromStringTests
{
    [Fact]
    public void FromString_ShouldParse_Correctly()
    {
        var version = SemVer.FromString("1.2.3");

        version.Major.ShouldBe(1);
        version.Minor.ShouldBe(2);
        version.Patch.ShouldBe(3);
    }

    [Fact]
    public void NoPreRelease_ShouldHave_NullSuffix()
    {
        var version = SemVer.FromString("1.1.1");

        version.Suffix.ShouldBe(null);
    }

    [Fact]
    public void PreRelease_ShouldHave_PreReleaseSuffix()
    {
        var version = SemVer.FromString("1.1.1-alpha.15");

        version.Suffix.ShouldBe("alpha.15");
    }
}
