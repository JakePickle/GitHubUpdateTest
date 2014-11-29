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
            int vers = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major;
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major);
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor);
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.MajorRevision);
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.MinorRevision);
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision);
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build);

            Console.Read();

            dir = dir.Substring(0, dir.Length - 20/*length of executables name including .exe*/);

            Task.Run(async () =>
            {
                var github = new GitHubClient(new ProductHeaderValue("name"));//Creates link to github

                var releases = await github.Release.GetAll("JakePickle", "GitHubUpdateTest");//gets all releases

                Console.WriteLine(releases[0].TagName);//Prints the tag of the latest release
                
                string fileName;
                Console.WriteLine(dir);
                string targetPath = dir.Substring(0,dir.Length-6)+"Test/";
                Console.WriteLine(targetPath);
                string destFile;

                if (System.IO.Directory.Exists(dir))
                {
                    string[] files = System.IO.Directory.GetFiles(dir);

                    // Copy the files and overwrite destination files if they already exist.
                    foreach (string s in files)
                    {
                        // Use static Path methods to extract only the file name from the path.
                        fileName = System.IO.Path.GetFileName(s);
                        destFile = System.IO.Path.Combine(targetPath, fileName);
                        System.IO.File.Copy(s, destFile, true);

                        Console.WriteLine("did file: " + fileName);
                    }
                }

                Process updater = new Process();
                updater.StartInfo.FileName = targetPath + "updaterUI.exe";
                updater.Start();

                System.Environment.Exit(1);
            });

            Console.ReadLine();
            
        }
    }
}
