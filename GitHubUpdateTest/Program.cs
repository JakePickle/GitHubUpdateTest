using System;
using System.Net;
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
                var github = new GitHubClient(new ProductHeaderValue("name"));//Creates link to github

                var user = await github.User.Get("tymorrow");//saves tymorrow's information to a user variable

                var repo = await github.Repository.GetBranch("tymorrow", "Launch-Pad", "master");//saves repository information to repo variable

                var releases = await github.Release.GetAll("MST-MRDT", "Rover-Engagement-Display");//gets all releases of GitHubUpdateTest

                Console.WriteLine(user.Followers + " people follow Ty Morrow");//prints number of people following tymorrow

                Console.WriteLine(repo.Commit.Sha);//prints sha for current commit in Launc-Pad

                Console.WriteLine(releases.Count);//prints number of releases for GitHubUpdateTest

                Console.WriteLine(releases[0].TagName);//Prints the tag of the latest version of GitHubTestUpdate

                Console.WriteLine("https://github.com/MST-MRDT/Rover-Engagement-Display/releases/download/" + releases[0].TagName + "/RED-" + releases[0].TagName + ".zip");

                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://github.com/MST-MRDT/Rover-Engagement-Display/releases/download/" + releases[0].TagName + "/RED-" + releases[0].TagName + ".zip", @"c:\users\ThePickle\downloads\RED-" + releases[0].TagName + ".zip");

                Console.WriteLine("Done Downloading File");

                //https://github.com/JakePickle/GitHubUpdateTest/releases/download/0.0.2/GitHubUpdateTest.exe

            }); 

            Console.WriteLine("Console Test");
            Console.ReadLine();
        }
    }
}
