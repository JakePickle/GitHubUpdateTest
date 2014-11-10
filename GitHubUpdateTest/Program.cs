using Octokit;
using System;
using System.Threading.Tasks;

namespace GitHubUpdateTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Task.Run(async () =>
            {
                var github = new GitHubClient(new ProductHeaderValue("name"));//Creates link to github

                var releases = await github.Release.GetAll("JakePickle", "GitHubUpdateTest");//gets all releases

                Console.WriteLine(releases[0].TagName);//Prints the tag of the latest release

            });

            Console.ReadLine();
            
        }
    }
}
