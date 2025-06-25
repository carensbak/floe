using System.Text.RegularExpressions;

using Floe.Core.Constants;

namespace Floe.Core.Extensions;

public static partial class StringExtensions
{
    [GeneratedRegex(@"\bv?(\d+)\.(\d+)\.(\d+)\b")] //See SYSLIB1045
    private static partial Regex SemverRegex();

    public static bool IsMasterBranch(this string branch)
    {
        return branch.Contains(Git.DefaultBranchNames.Master);
    }

    public static bool IsDevBranch(this string branch)
    {
        return branch.Contains(Git.DefaultBranchNames.Dev);
    }

    public static bool IsFeatureBranch(this string branch)
    {
        return branch.Contains(Git.DefaultBranchNames.Feature);
    }

    public static bool IsFixBranch(this string branch)
    {
        return branch.Contains(Git.DefaultBranchNames.Fix);
    }

    public static bool IsReleaseBranch(this string branch)
    {
        return branch.Contains(Git.DefaultBranchNames.Release);
    }

    public static bool ContainsSemver(this string branch)
    {
        return SemverRegex().IsMatch(branch);
    }

    public static string? TryGetSemver(this string branch)
    {
        var match = SemverRegex().Match(branch);
        return match.Success
            ? match.Value
            : null;
    }
}