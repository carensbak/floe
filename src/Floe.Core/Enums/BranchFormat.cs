using Floe.Core.Models;

namespace Floe.Core.Enums;

public enum BranchFormat
{
    RefnameShort
}

public static class BranchFormatExtensions
{
    public static string ToCommandString(this BranchFormat format)
    {
        return format switch
        {
            BranchFormat.RefnameShort => Git.BranchFlags.FormatRefnameShort,
            _ => throw new ArgumentOutOfRangeException(nameof(format))
        };
    }
}