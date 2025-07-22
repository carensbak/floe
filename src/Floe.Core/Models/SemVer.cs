using Floe.Core.Extensions;

using VersionNotFoundException = Floe.Core.Exceptions.VersionNotFoundException;

namespace Floe.Core.Models;

public class SemVer : IComparable<SemVer>
{
	public required int Major { get; set; }
	public required int Minor { get; set; }
	public required int Patch { get; set; }
	public string? Suffix { get; set; }

	public static SemVer LatestRelease => GetLatestVersion(includePreReleases: false);
	public static SemVer LatestVersion => GetLatestVersion(includePreReleases: true);
	public override string ToString() => !Suffix.IsNullOrWhiteSpace() ? $"{Major}.{Minor}.{Patch}-{Suffix}" : $"{Major}.{Minor}.{Patch}";

	public static SemVer FromString(string str)
	{
		var semverParts = str.Split('-', 2);
		var semverString = semverParts[0];
		var semverArray = semverString.Split('.');

		var semverSuffix = semverParts.Length == 2
			? semverParts[1]
			: "";

		var semver = new SemVer
		{
			Major = Int32.Parse(semverArray[0]),
			Minor = Int32.Parse(semverArray[1]),
			Patch = Int32.Parse(semverArray[2])
		};

		if (!semverSuffix.IsNullOrWhiteSpace())
			semver.Suffix = semverSuffix;

		return semver;
	}

	public int CompareTo(SemVer? other)
	{
		if (this is null && other is null)
			return 0;

		if (this is null && other is not null)
			return -1;

		if (this is not null && other is null)
			return 1;

		var result = Major.CompareTo(other!.Major);
		if (result != 0)
			return result;

		result = Minor.CompareTo(other.Minor);
		if (result != 0)
			return result;

		result = Patch.CompareTo(other.Patch);
		if (result != 0)
			return result;

		//handle suffix - No suffix is considered to be a later release.
		if (Suffix.IsNullOrWhiteSpace() && !other.Suffix.IsNullOrWhiteSpace())
			return 1;

		if (!Suffix.IsNullOrWhiteSpace() && other.Suffix.IsNullOrWhiteSpace())
			return -1;

		return string.Compare(Suffix, other.Suffix, StringComparison.Ordinal);
	}

	internal static SemVer GetLatestVersion(bool includePreReleases)
	{
		var tagStrings = Git.Tags
			.Select(t => SemVer.FromString(t))
			.ToList();

		return GetLatestVersion(includePreReleases, tagStrings);
	}

	internal static SemVer GetLatestVersion(bool includePreReleases, List<SemVer> versions)
	{
		var latest = includePreReleases
			? versions.Max()
			: SemVer.GetReleaseVersions(versions).Max();

		return latest is not null ? latest : throw new VersionNotFoundException(includePreReleases, versions);
	}

	internal static List<SemVer?> GetReleaseVersions(List<SemVer> unfilteredVersions)
	{
		return unfilteredVersions
			.Where(v => v.Suffix.IsNullOrWhiteSpace())
			.DefaultIfEmpty(null)
			.ToList();
	}
}