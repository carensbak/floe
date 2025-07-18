using Floe.Core.Logging;
using Floe.Core.Models;

namespace Floe.Cli.Commands;

internal static partial class Commands
{
    internal static class Release
    {
        public static void Latest() => Logger.LogSuccess($"Latest version: '{SemVer.LatestVersion}'");

        public static void Major()
        {
            var latestRelease = SemVer.LatestRelease;
            latestRelease.Major++;
            latestRelease.Minor = 0;
            latestRelease.Patch = 0;

            var bumpedVersion = latestRelease.ToString();

            Commands.Branch.Create(
                branch: $"{Git.Branches.Release}/{bumpedVersion}",
                switchToBranch: true,
                pushToRemote: true);
        }

        public static void Minor()
        {
            var latestRelease = SemVer.LatestRelease;
            latestRelease.Minor++;
            latestRelease.Patch = 0;

            var bumpedVersion = latestRelease.ToString();

            Commands.Branch.Create(
                branch: $"{Git.Branches.Release}/{bumpedVersion}",
                switchToBranch: true,
                pushToRemote: true);
        }

        public static void Patch()
        {
            var latestRelease = SemVer.LatestRelease;
            latestRelease.Patch++;
            var bumpedVersion = latestRelease.ToString();

            Commands.Branch.Create(
                branch: $"{Git.Branches.Release}/{bumpedVersion}",
                switchToBranch: true,
                pushToRemote: true);
        }
    }
}