namespace Floe.Core.Constants;

public static class Git
{
    public const string BaseName = "git";
    public const string Head = "HEAD";
    public const string Merge = "merge";
    public const string Branch = "branch";

    public static class MergeFlags
    {
        public const string NoFastForward = "--no-ff";
        public const string Message = "--message";
    }

    public static class BranchFlags
    {
        public const string FormatRefnameShort = "--format=\"%(refname:short)\"";
        public const string ShowCurrent = "--show-current";
    }
}
