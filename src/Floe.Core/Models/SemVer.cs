using Floe.Core.Extensions;

namespace Floe.Core.Models;

public class SemVer : IComparable<SemVer>
{
    public required int Major { get; set; }
    public required int Minor { get; set; }
    public required int Patch { get; set; }
    public string? Suffix { get; set; }

    public override string ToString()
    {
        return $"{Major}.{Minor}.{Patch}";
    }

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

    public static SemVer GetLatestVersion(bool includePreReleases)
    {
        var allTagsString = Git.GetAllTags();
        var allTagsSemver = allTagsString
            .Select(t => SemVer.FromString(t))
            .ToList();

        return GetLatestVersion(includePreReleases, allTagsSemver);
    }

    public static SemVer GetLatestVersion(bool includePreReleases, List<SemVer> versions)
    {
        var latest = includePreReleases
            ? versions.Max()
            : versions
                .Where(t => t.Suffix.IsNullOrWhiteSpace())
                .DefaultIfEmpty(null)
                .Max();

        return latest ?? new SemVer { Major = 1, Minor = 0, Patch = 0 };
    }
}