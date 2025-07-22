namespace Floe.Core.Models;

public class ProcessResult
{
	public required List<string> StdOutLines { get; init; }
	public required List<string> ErrorLines { get; init; }
	public required int ExitCode { get; init; }

	public bool WasSuccess => ExitCode is 0;
	public string FirstLine => StdOutLines[0];
}
