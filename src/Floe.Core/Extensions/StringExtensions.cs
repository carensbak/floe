using System.Text.RegularExpressions;

using Floe.Core.Models;

namespace Floe.Core.Extensions;

public static partial class StringExtensions
{
    [GeneratedRegex(@"\bv?(\d+)\.(\d+)\.(\d+)\b")] //See SYSLIB1045
    private static partial Regex SemverRegex();

    public static bool IsMasterBranch(this string branch) => branch.Contains(Git.BranchNames.Master);
    public static bool IsDevBranch(this string branch) => branch.Contains(Git.BranchNames.Dev);
    public static bool IsFeatureBranch(this string branch) => branch.Contains(Git.BranchNames.Feature);
    public static bool IsFixBranch(this string branch) => branch.Contains(Git.BranchNames.Fix);
    public static bool IsReleaseBranch(this string branch) => branch.Contains(Git.BranchNames.Release);
    public static bool ContainsSemver(this string branch) => SemverRegex().IsMatch(branch);

    public static string? TryGetSemver(this string branch)
    {
        var match = SemverRegex().Match(branch);
        return match.Success
            ? match.Value
            : null;
    }
}