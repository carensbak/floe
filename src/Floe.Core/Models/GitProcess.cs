using System.Diagnostics;
using System.Text;

namespace Floe.Core.Models;

public abstract class GitProcess
{
	private List<string> _outputLines { get; } = [];
	private List<string> _outputErrors { get; } = [];
	private string? _outputLine { get; set; }
	private string? _errorLine { get; set; }

	protected StringBuilder ArgsBuilder { get; } = new();

	private Process StartProcess(string command, string args)
	{
		var process = new Process
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = Git.BaseName,
				Arguments = string.Join(' ', command, args),
				RedirectStandardError = true,
				RedirectStandardOutput = true,
			}
		};

		process.Start();

		return process;
	}

	protected abstract GitProcess AddArgument(string arg);
	public abstract ProcessResult Execute();
	protected virtual ProcessResult Execute(string command, string args)
	{
		using var process = StartProcess(command, args);
		ProcessOutput(process);

		process.WaitForExit();
		LogErrorsOrOutput();

		return new ProcessResult { StdOutLines = _outputLines, ErrorLines = _outputErrors, ExitCode = process.ExitCode };
	}

	public abstract Task<ProcessResult> ExecuteAsync();
	protected virtual async Task<ProcessResult> ExecuteAsync(string command, string args)
	{
		using var process = StartProcess(command, args);
		await ProcessOutputAsync(process);

		await process.WaitForExitAsync();
		LogErrorsOrOutput();

		return new ProcessResult { StdOutLines = _outputLines, ErrorLines = _outputErrors, ExitCode = process.ExitCode };
	}

	private void ProcessOutput(Process process)
	{
		using var oReader = process.StandardOutput;
		while ((_outputLine = oReader.ReadLine()) != null)
			_outputLines.Add(_outputLine);

		using var eReader = process.StandardError;
		while ((_errorLine = eReader.ReadLine()) != null)
			_outputErrors.Add(_errorLine);
	}

	private async Task ProcessOutputAsync(Process process)
	{
		Task readStdOutAsync = Task.Run(async () =>
		{
			using var reader = process.StandardOutput;
			while ((_outputLine = await reader.ReadLineAsync()) != null)
				_outputLines.Add(_outputLine);
		});

		Task readStdErrorAsync = Task.Run(async () =>
		{
			using var reader = process.StandardError;
			while ((_errorLine = await reader.ReadLineAsync()) != null)
				_outputErrors.Add(_errorLine);
		});

		await Task.WhenAll(readStdOutAsync, readStdErrorAsync);
	}

	private void LogErrorsOrOutput()
	{
		if (_outputErrors.Count > 0)
			_outputErrors.ForEach(el => Console.WriteLine(el));
		else
			_outputLines.ForEach(ol => Console.WriteLine(ol));
	}
}