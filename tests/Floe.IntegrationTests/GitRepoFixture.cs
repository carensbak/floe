using Floe.Cli.IntegrationTests;
using Floe.Core.Models;

[assembly: AssemblyFixture(typeof(GitRepoFixture))]
namespace Floe.Cli.IntegrationTests;

public class GitRepoFixture
{
    public GitRepoFixture()
    {
        var tempRepoPath = Path.Combine(AppContext.BaseDirectory, $"tmp-{Guid.NewGuid()}");
        Directory.CreateDirectory(tempRepoPath);
        Directory.SetCurrentDirectory(tempRepoPath);

        Git.Init()
            .Execute();

        Git.Commit()
            .WithMessage("initial commit")
            .AllowEmpty()
            .Execute();

        Git.Tag("1.0.0", "1.1.0", "1.1.0-alpha.1", "1.1.0-alpha.23", "1.1.1", "1.1.2", "1.1.3",
                "1.1.4", "1.2.0", "1.2.1", "1.2.2", "1.2.3", "1.3.0", "1.3.1", "1.4.0-alpha.0");
    }
}
