using System.Text;

namespace Floe.Core.Extensions;

public static class StringBuilderExtensions
{
	public static StringBuilder AppendArgument(this StringBuilder builder, string text)
	{
		if (builder.Length > 0)
			builder.Append(' ');

		builder.Append(text);

		return builder;
	}

	public static string Build(this StringBuilder builder) => builder.ToString();
}