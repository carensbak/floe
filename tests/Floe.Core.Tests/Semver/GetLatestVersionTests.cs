using Floe.Core.Exceptions;
using Floe.Core.Models;

namespace Floe.Core.Tests.Semver;

public class GetLatestVersionTests : SemVer
{
	[Fact]
	public void LatestMajor_ShouldBe_Returned()
	{
		var versions = new List<SemVer>()
		{
			new SemVer{ Major = 0, Minor = 0, Patch = 0 },
			new SemVer{ Major = 2, Minor = 0, Patch = 0 },
			new SemVer{ Major = 1, Minor = 0, Patch = 0 }
		};

		var latest = SemVer.GetLatestVersion(false, versions);

		latest.Major.ShouldBe(2);
	}

	[Fact]
	public void LatestMinor_ShouldBe_Returned()
	{
		var versions = new List<SemVer>()
		{
			new SemVer{ Major = 0, Minor = 3, Patch = 0 },
			new SemVer{ Major = 0, Minor = 5, Patch = 0 },
			new SemVer{ Major = 0, Minor = 1, Patch = 0 }
		};

		var latest = SemVer.GetLatestVersion(false, versions);

		latest.Minor.ShouldBe(5);
	}

	[Fact]
	public void LatestPatch_ShouldBe_Returned()
	{
		var versions = new List<SemVer>()
		{
			new SemVer{ Major = 0, Minor = 0, Patch = 5 },
			new SemVer{ Major = 0, Minor = 0, Patch = 10 },
			new SemVer{ Major = 0, Minor = 0, Patch = 3 }
		};

		var latest = SemVer.GetLatestVersion(false, versions);

		latest.Patch.ShouldBe(10);
	}

	[Fact]
	public void FullRelease_ShouldBe_Returned()
	{
		var versions = new List<SemVer>()
		{
			new SemVer{ Major = 9, Minor = 9, Patch = 9 },
			new SemVer{ Major = 9, Minor = 9, Patch = 9, Suffix = "alpha.9" },
	   };

		var latest = SemVer.GetLatestVersion(true, versions);

		latest.Suffix.ShouldBe(null);
	}

	[Fact]
	public void EmptyCollection_ShouldThrow_VersionNotFound()
	{
		var versions = new List<SemVer>();

		Should.Throw<VersionNotFoundException>(() => SemVer.GetLatestVersion(false, versions));
	}

	[Fact]
	public void OnlyPreReleases_And_DontIncludePreReleases_ShouldThrow_VersionNotFound()
	{
		var versions = new List<SemVer>()
		{
			new SemVer { Major = 1, Minor = 1, Patch = 0, Suffix = "alpha.1" },
			new SemVer { Major = 1, Minor = 1, Patch = 0, Suffix = "alpha.23" },
			new SemVer { Major = 1, Minor = 4, Patch = 0, Suffix = "alpha.0" },
		};

		Should.Throw<VersionNotFoundException>(() => SemVer.GetLatestVersion(false, versions));
	}
}
