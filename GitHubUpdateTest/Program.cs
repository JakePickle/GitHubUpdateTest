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
                Console.WriteLine(user.Followers + " people follow Ty Morrow");
            }); 

            Console.Write("Console Test");
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
