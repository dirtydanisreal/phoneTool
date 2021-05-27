using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Net;
using System.Drawing;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using phoneTool.Properties;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security;
using Microsoft.Win32;

namespace phoneTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //FileUnblocker fub = new FileUnblocker();
            //string[] list = Directory.EnumerateFiles("./", "*.*", SearchOption.AllDirectories).ToArray();
            //foreach (string s in list)
            //{
            //    Console.WriteLine($"Filename - {s} :: Unblocked -> {fub.Unblock(s)}");
            //}
            
            dirCheck();
            string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool");
            string filePath = Path.Combine(dataPath, "numberData.csv");
            MonitorDirectory(dataPath);
            Application.Run(new Form1());
            
            
            //MonitorDirectory(appData);

        }

        
        

        private static void MonitorDirectory(string path)
        {
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

            fileSystemWatcher.Path = path;

            fileSystemWatcher.Created += FileSystemWatcher_Created;

            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;

            fileSystemWatcher.Changed += FileSystemWatcher_Changed;

            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;

            fileSystemWatcher.EnableRaisingEvents = true;
        }

        

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)

        {
            
        }

        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)

        {

            Console.WriteLine("File created: {0}", e.Name);

        }

        private static void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)

        {

            Console.WriteLine("File renamed: {0}", e.Name);

        }

        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)

        {
            string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool");
            string filePath = Path.Combine(dataPath, "numberData.csv");
            WriteResourceToFile("phoneTool.numberData.csv", filePath);

        }

        static void dirCheck()
        {
            string user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string localAppData = Environment.ExpandEnvironmentVariables("%localappdata%");
            string roamingAppData = Environment.ExpandEnvironmentVariables("%appdata%");
            string userName = Environment.ExpandEnvironmentVariables("%username%");
            string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool");
            string filePath = Path.Combine(dataPath, "numberData.csv");
            WebClient Client = new WebClient();

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+"AppData\\Local\\phoneTool"))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool"));

                WriteResourceToFile("phoneTool.numberData.csv", filePath);

            }


        }

        public  static void WriteResourceToFile(string resourceName, string fileName)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
        }

       



    }
}
