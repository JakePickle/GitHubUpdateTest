using Octokit;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GitHubUpdateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.Substring(8);

            Task.Run(async () =>
            {
                var github = new GitHubClient(new ProductHeaderValue("name"));//Creates link to github

                var releases = await github.Release.GetAll("JakePickle", "GitHubUpdateTest");//gets all releases

                Console.WriteLine(releases[0].TagName);//Prints the tag of the latest release

                Process notePad = new Process();
                Console.WriteLine(dir.Substring(0, dir.Length - 20/*length of executables name including .exe*/) + "updater.exe");
                notePad.StartInfo.FileName = dir.Substring(0, dir.Length - 20/*length of executables name including .exe*/)+"/updater.exe";
                notePad.Start();

                System.Environment.Exit(1);
            });

            Console.ReadLine();
            
        }
    }
}
