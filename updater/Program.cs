using Octokit;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

namespace updater
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.Substring(8);
            string zipPath, extractPath;
            Console.WriteLine(dir);

            Task.Run(async () =>
            {
                var github = new GitHubClient(new ProductHeaderValue("name"));//Creates link to github

                var releases = await github.Release.GetAll("JakePickle", "GitHubUpdateTest");//gets all releases of RED

                Console.WriteLine(releases[0].TagName);//Prints the tag of the latest release

                Console.WriteLine("Downloading Latest Version(" + releases[0].TagName + ") of GitHubUpdateTest");

                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://github.com/JakePickle/GitHubUpdateTest/releases/download/" + releases[0].TagName + "GitHubUpdateTest.exe",// + releases[0].TagName + ".zip",
                    @dir.Substring(0, dir.Length - 20/*length of executables name including .exe*/) + releases[0].TagName + ".zip");

                Console.WriteLine("Done Downloading File");
                zipPath = dir.Substring(0, dir.Length - 20/*length of executables name including .exe*/) + releases[0].TagName + ".zip";
                extractPath = dir.Substring(0, dir.Length - 20/*length of executables name including .exe*/);
                Console.WriteLine("Extracting Archive");
                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        entry.ExtractToFile(Path.Combine(extractPath, entry.FullName), true/*overwrite*/);
                    }
                }
                Console.WriteLine("Extraction Complete\nPress enter to end.");

            });

            Console.WriteLine("Console Test");
            Console.ReadLine();
        }
    }
}
