namespace Floe.Core.Constants;

public static class Git
{
    public const string BaseName = "git";
    public const string Head = "HEAD";
    public const string Origin = "origin";
    public const string Push = "push";
    public const string Pull = "pull";
    public const string Merge = "merge";
    public const string Branch = "branch";
    public const string Fetch = "fetch";
    public const string Tag = "tag";

    public static class DefaultBranchNames
    {
        public const string Master = "master";
        public const string Dev = "dev";
        public const string Fix = "fix";
        public const string Feature = "feature";
        public const string Release = "release";
    }

    public static class PushFlags
    {
        public const string SetUpstream = "--set-upstream";
    }

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
    public static class FetchFlags
    {
        public const string All = "--all";
    }
}
