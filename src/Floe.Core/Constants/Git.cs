namespace Floe.Core.Constants;

public static class Git
{
    public const string BaseName = "git";

    public static class Merge
    {
        public const string BaseName = "merge";

        public static class Flags
        {
            public const string NoFastForward = "--no-ff";
            public const string Message = "--message";
        }
    }
}
