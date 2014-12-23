using Octokit;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace updaterUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string dir = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.Substring(8);
            Text.Text = dir.Substring(0,dir.Length-19)+"Test/";
            string zipPath, delDir;
            string[] zipList, delList;
            dir = dir.Substring(0, dir.Length - 13/*length of executables name including .exe*/);

            Task.Run(async () =>
            {
                var github = new GitHubClient(new ProductHeaderValue("name"));//Creates link to github

                var releases = await github.Release.GetAll("JakePickle", "GitHubUpdateTest");//gets all releases of RED   

                delDir = dir.Substring(0, dir.Length - 5) + "Debug/";
                Console.WriteLine("Deleting contents of:" + delDir);
                delList = Directory.GetFiles(delDir);

                foreach (string f in delList)
                {
                    File.Delete(f);
                }

                Console.WriteLine("Deleted");

                await this.Dispatcher.BeginInvoke((Action)delegate()
                {
                    ProgressBar.Value = 20;
                });

                Console.WriteLine("Downloading Latest Version(" + releases[0].TagName + ") of GitHubUpdateTest");

                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://github.com/JakePickle/GitHubUpdateTest/releases/download/" + releases[0].TagName + "/GitHubUpdateTest.zip",
                    dir + releases[0].TagName + ".zip");

                Console.WriteLine("Done Downloading File");

                await this.Dispatcher.BeginInvoke((Action)delegate()
                {
                    ProgressBar.Value = 50;
                });

                zipPath = dir + releases[0].TagName + ".zip";
                Console.WriteLine(dir);
                Console.WriteLine("Extracting Archive");

                ZipFile.ExtractToDirectory(zipPath, delDir);

                Console.WriteLine("Extraction Complete");

                await this.Dispatcher.BeginInvoke((Action)delegate()
                {
                    ProgressBar.Value = 75;
                });

                zipList = Directory.GetFiles(dir, "*.zip");

                foreach (string f in zipList)
                {
                    File.Delete(f);
                }

                await this.Dispatcher.BeginInvoke((Action)delegate()
                {
                    ProgressBar.Value = 95;
                });

                Process updateTest = new Process();
                updateTest.StartInfo.FileName = delDir + "GitHubUpdateTest.exe";
                updateTest.Start();

                await this.Dispatcher.BeginInvoke((Action)delegate()
                {
                    ProgressBar.Value = 100;
                });

                System.Environment.Exit(1);
            });
        }
    }
}
