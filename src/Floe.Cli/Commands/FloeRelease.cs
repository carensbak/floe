using Floe.Core.Logging;
using Floe.Core.Models;

namespace Floe.Cli.Commands;

internal static partial class Command
{
	internal static class Release
	{
		internal static void Latest() => Logger.LogSuccess($"Latest version: '{SemVer.LatestVersion}'");

		internal static void Major(bool? switchToBranch = null, bool? pushToRemote = null)
		{
			var latestRelease = SemVer.LatestRelease;
			latestRelease.Major++;
			latestRelease.Minor = 0;
			latestRelease.Patch = 0;

			var bumpedVersion = latestRelease.ToString();

			Command.Branch.Create(
				branch: $"{Git.Branches.Release}/{bumpedVersion}",
				switchToBranch ?? true,
				pushToRemote ?? true);
		}

		internal static void Minor(bool? switchToBranch = null, bool? pushToRemote = null)
		{
			var latestRelease = SemVer.LatestRelease;
			latestRelease.Minor++;
			latestRelease.Patch = 0;

			var bumpedVersion = latestRelease.ToString();

			Command.Branch.Create(
				branch: $"{Git.Branches.Release}/{bumpedVersion}",
				switchToBranch ?? true,
				pushToRemote ?? true);
		}

		internal static void Patch(bool? switchToBranch = null, bool? pushToRemote = null)
		{
			var latestRelease = SemVer.LatestRelease;
			latestRelease.Patch++;
			var bumpedVersion = latestRelease.ToString();

			Command.Branch.Create(
				branch: $"{Git.Branches.Release}/{bumpedVersion}",
				switchToBranch ?? true,
				pushToRemote ?? true);
		}
	}
}