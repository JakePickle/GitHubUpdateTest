using Octokit;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace GitHubUpdateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.Substring(8);
            string running = System.AppDomain.CurrentDomain.FriendlyName;
            string binDir = dir.Substring(0, dir.Length - running.Length);
            int vers = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major;
            int major = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major;
            int minor = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor;
            int majorRev = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.MajorRevision;//Dont know what these
            int minorRev = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.MinorRevision;//two are for
            int Revision = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision;
            int build = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;
            Ping pingClass = new Ping();
            PingReply reply;

            dir = dir.Substring(0, dir.Length-1);
            while (dir[dir.Length - 1] != '/')
            {
                dir = dir.Substring(0, dir.Length - 1);
            }

            Console.WriteLine(major + "." + minor + "." + build + "." + Revision);

            reply = pingClass.Send("github.com");
            if (reply.Status == IPStatus.Success)
            {         
                Task.Run(async () =>
                {
                    var github = new GitHubClient(new ProductHeaderValue("name"));//Creates link to github

                    var releases = await github.Release.GetAll("JakePickle", "GitHubUpdateTest");//gets all releases

                    Console.WriteLine(releases[0].TagName);//Prints the tag of the latest release

                    /*string[] nums = releases[0].TagName.Split('.'); Code to check if update is needed, needs to be commented for testing.

                    if(int.Parse(nums[0])<major)
                    {
                        System.Environment.Exit(1);
                    }

                    if (int.Parse(nums[1]) < minor)
                    {
                        System.Environment.Exit(1);
                    }

                    if (int.Parse(nums[2]) < build)
                    {
                        System.Environment.Exit(1);
                    }*/
                
                    string fileName;
                    Console.WriteLine(dir);

                    string tarDir = dir.Substring(0, dir.Length - 1);
                    while (tarDir[tarDir.Length - 1] != '/')
                    {
                        tarDir = tarDir.Substring(0, tarDir.Length - 1);
                    }

                    tarDir += "Test/";
                    Console.WriteLine(tarDir);
                    string destFile;

                    if (!System.IO.Directory.Exists(tarDir))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(tarDir);
                        Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(tarDir));
                    }

                    if (System.IO.Directory.Exists(dir))
                    {
                        string[] files = System.IO.Directory.GetFiles(dir);

                        // Copy the files and overwrite destination files if they already exist.
                        foreach (string s in files)
                        {
                            // Use static Path methods to extract only the file name from the path.
                            fileName = System.IO.Path.GetFileName(s);
                            destFile = System.IO.Path.Combine(tarDir, fileName);
                            System.IO.File.Copy(s, destFile, true);

                            Console.WriteLine("did file: " + fileName);
                        }
                    }

                    Process updater = new Process();
                    updater.StartInfo.FileName = tarDir + "updaterUI.exe";
                    updater.Start();

                    System.Environment.Exit(1);
                });
            }
            Console.ReadLine();
            
        }
    }
}
