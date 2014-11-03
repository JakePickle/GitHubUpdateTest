using System;
using System.Threading.Tasks;
using Octokit;

namespace GitHubUpdateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));
                var user = await github.User.Get("tymorrow");
                var repo = await github.Repository.GetBranch("tymorrow", "Launch-Pad", "master");
                var releases = await github.Release.GetAll("JakePickle", "GitHubUpdateTest");
                Console.WriteLine(user.Followers + " people follow Ty Morrow");
                Console.WriteLine(repo.Commit.Sha);
                Console.WriteLine(releases.Count);
                Console.WriteLine(releases[0].TagName);
            }); 

            Console.Write("Console Test");
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
