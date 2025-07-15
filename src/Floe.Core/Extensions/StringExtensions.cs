using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

using Floe.Core.Models;

namespace Floe.Core.Extensions;

public static partial class StringExtensions
{
    [GeneratedRegex(@"\bv?(\d+)\.(\d+)\.(\d+)\b")] //See SYSLIB1045
    private static partial Regex SemverRegex();

    public static bool IsMasterBranch(this string branch) => branch.Contains(Git.Branches.Master);
    public static bool IsDevBranch(this string branch) => branch.Contains(Git.Branches.Dev);
    public static bool IsFeatureBranch(this string branch) => branch.Contains(Git.Branches.Feature);
    public static bool IsFixBranch(this string branch) => branch.Contains(Git.Branches.Fix);
    public static bool IsReleaseBranch(this string branch) => branch.Contains(Git.Branches.Release);
    public static bool ContainsSemver(this string branch) => SemverRegex().IsMatch(branch);

    public static string? TryGetSemver(this string branch)
    {
        var match = SemverRegex().Match(branch);
        return match.Success
            ? match.Value
            : null;
    }

    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);
}