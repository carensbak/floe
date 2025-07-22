using System.Text;

using Floe.Core.Models;

namespace Floe.Core.Exceptions;

public class VersionNotFoundException(bool includePreReleases, List<SemVer> versions) : Exception(GetExceptionMessage(includePreReleases, versions))
{
	private static string GetExceptionMessage(bool includePreReleases, List<SemVer> versions)
	{
		var sb = new StringBuilder();
		sb.AppendLine($"Could not find a valid version in collection '{{{nameof(versions)}}}'.");
		sb.AppendLine($"Include prereleases: '{{{includePreReleases}}}'");
		sb.AppendLine($"Passed versions:");
		versions.ForEach(v => sb.AppendLine(v.ToString()));

		return sb.ToString();
	}
}
