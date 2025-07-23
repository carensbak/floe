using System.Diagnostics;

namespace Floe.Core.Extensions;

public static class ProcessExtensions
{
	public static List<string> StdOutToList(this Process process)
	{
		var items = new List<string>();

		while (!process.StandardOutput.EndOfStream)
		{
			var line = process.StandardOutput.ReadLine()?.Trim();
			if (!string.IsNullOrEmpty(line))
			{
				items.Add(line);
			}
		}

		return items;
	}

	public static string GetStdOutFirstLine(this Process process)
	{
		var item = "";
		while (!process.StandardOutput.EndOfStream && item.IsNullOrWhiteSpace())
		{
			item = process.StandardOutput.ReadLine()?.Trim() ?? "";
		}

		return item;
	}
}
