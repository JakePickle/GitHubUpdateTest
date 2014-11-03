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
                var user = await github.User.Get("half-ogre");
                Console.WriteLine(user.Followers + " folks love the half ogre!");
            }); 

            Console.Write("Console Test");
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
