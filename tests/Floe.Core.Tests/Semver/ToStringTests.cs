using Floe.Core.Models;

namespace Floe.Core.Tests.Semver;

public class ToStringTests
{
	[Fact]
	public void PreReleaseToString_Should_ParseCorrectly_And_Contain_Suffix()
	{
		var prereleaseVersion = new SemVer { Major = 1, Minor = 1, Patch = 1, Suffix = "alpha.57" };
		var versionString = prereleaseVersion.ToString();

		versionString.ShouldContain('-');
		versionString.ShouldMatch(@"\d+.\d+.\d+?[-][a-zA-Z]+?.\d+");
		versionString.ShouldBe("1.1.1-alpha.57");
	}

	[Fact]
	public void ReleaseToString_Should_ParseCorrectly_And_NotContain_Suffix()
	{
		var releaseVersion = new SemVer { Major = 9, Minor = 9, Patch = 9 };
		var versionString = releaseVersion.ToString();

		versionString.ShouldNotContain("-");
		versionString.ShouldMatch(@"\d+.\d+.\d+");
		versionString.ShouldBe("9.9.9");
	}
}
