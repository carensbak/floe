using Floe.Core.Models;

namespace Floe.Cli.Commands;

internal static partial class Commands
{
    internal static class Release
    {
        public static void Latest()
        {
            var latestVersion = SemVer.GetLatestVersion(includePreReleases: false);

            Console.WriteLine($"Latest version: '{latestVersion}'");
        }

        public static void Major()
        {
            var latestVersion = SemVer.GetLatestVersion(includePreReleases: false);
            latestVersion.Major++;
            latestVersion.Minor = 0;
            latestVersion.Patch = 0;

            var bumpedVersion = latestVersion.ToString();

            Commands.Branch.Create(
                branch: $"{Git.Branches.Release}/{bumpedVersion}",
                switchToBranch: true,
                pushToRemote: true);
        }

        public static void Minor()
        {
            var latestVersion = SemVer.GetLatestVersion(includePreReleases: false);
            latestVersion.Minor++;
            latestVersion.Patch = 0;

            var bumpedVersion = latestVersion.ToString();

            Commands.Branch.Create(
                branch: $"{Git.Branches.Release}/{bumpedVersion}",
                switchToBranch: true,
                pushToRemote: true);
        }

        public static void Patch()
        {
            var latestVersion = SemVer.GetLatestVersion(includePreReleases: false);
            latestVersion.Patch++;
            var bumpedVersion = latestVersion.ToString();

            Commands.Branch.Create(
                branch: $"{Git.Branches.Release}/{bumpedVersion}",
                switchToBranch: true,
                pushToRemote: true);
        }
    }
}