using Octokit;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

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
            Text.Text = "test";
            string dir = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.Substring(8);
            string zipPath, delDir;
            dir = dir.Substring(0, dir.Length - 13/*length of executables name including .exe*/);
            Text.Text = dir;

            Task.Run(async () =>
            {
                var github = new GitHubClient(new ProductHeaderValue("name"));//Creates link to github

                var releases = await github.Release.GetAll("JakePickle", "GitHubUpdateTest");//gets all releases of RED

                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://github.com/JakePickle/GitHubUpdateTest/releases/download/" + releases[0].TagName + "/GitHubUpdateTest.zip",
                    dir + releases[0].TagName + ".zip");
            });
        }
    }
}
