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
using System.Security.Cryptography;


namespace phoneTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]


        static void Main(string[] args)
        {
            try
            {
                Helper.DeleteFile(Application.ExecutablePath + ":Zone.Identifier");
            }
            catch { }
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //FileUnblocker fub = new FileUnblocker();
            //string[] list = Directory.EnumerateFiles("./", "*.*", SearchOption.AllDirectories).ToArray();
            //foreach (string s in list)
            //{
            //    Console.WriteLine($"Filename - {s} :: Unblocked -> {fub.Unblock(s)}");
            //}
            
            //dirCheck();
            string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool");
            string filePath = Path.Combine(dataPath, "numberData.csv");
            string networkPath = "\\ukhcdata\\dept\\Trauma Services\\Trauma Surgical Clerks\\numberData";
            string netPath = ConvertUNCPathToPhysicalPath("\\ukhcdata\\dept\\Trauma Services\\Trauma Surgical Clerks\\numberData");
            checkUpdate();
           // MonitorDirectory(dataPath);
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

            fileSystemWatcher.IncludeSubdirectories = true;

            fileSystemWatcher.Filter = "*.txt";
            
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
        }

        

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)

        {
            
        }

        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)

        {
            
            

        }

        private static void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)

        {

            

        }

        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)

        {
            string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool");
            string filePath = Path.Combine(dataPath, "numberData.csv");
            //WriteResourceToFile("phoneTool.numberData.csv", filePath);
            checkUpdate();

        }

        static void dirCheck()
        {
            string user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string localAppData = Environment.ExpandEnvironmentVariables("%localappdata%");
            string roamingAppData = Environment.ExpandEnvironmentVariables("%appdata%");
            string userName = Environment.ExpandEnvironmentVariables("%username%");
            string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool");
            string netPath = ConvertUNCPathToPhysicalPath("\\ukhcdata\\dept\\Trauma Services\\Trauma Surgical Clerks\\numberData");
            string filePath = Path.Combine(dataPath, "numberData.csv");
            WebClient Client = new WebClient();

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "AppData\\Local\\phoneTool"))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool"));

                WriteResourceToFile("phoneTool.numberData.csv", filePath);

                checkUpdate();
            }
            else
            { 
                checkUpdate();
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

       public static void checkUpdate()
        {
            WebClient Client = new WebClient();
            string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool");
            
            string temp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool\\temp");
            string tempPath = Path.Combine(temp, "numberData.csv");
            string filePath = Path.Combine(dataPath, "numberData.csv");
            string networkPath = "\\ukhcdata\\dept\\Trauma Services\\Trauma Surgical Clerks\\numberData";
            string fileNetPath = Path.Combine(networkPath, "numberData.csv");
            //if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "AppData\\Local\\phoneTool\\temp"))
            //{
            //    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool\\temp"));
            //}
            
            //Client.DownloadFile("https://raw.githubusercontent.com/dirtydanisreal/numberData/master/numberData.csv", tempPath);
            //if(CompareFileHashes(filePath, tempPath) == false)
            //{

            //    File.Replace(tempPath, filePath, "numberData.bak.csv");
            //    File.Delete("numberData.bak.csv");
            //}
            //else
            //{

            //}

             
        }

        private static bool CompareFileHashes(string fileName1, string fileName2)
        {
            // Compare file sizes before continuing. 
            // If sizes are equal then compare bytes.
            if (CompareFileSizes(fileName1, fileName2))
            {
                // Create an instance of System.Security.Cryptography.HashAlgorithm
                HashAlgorithm hash = HashAlgorithm.Create();

                // Declare byte arrays to store our file hashes
                byte[] fileHash1;
                byte[] fileHash2;

                // Open a System.IO.FileStream for each file.
                // Note: With the 'using' keyword the streams 
                // are closed automatically.
                using (FileStream fileStream1 = new FileStream(fileName1, FileMode.Open),
                                  fileStream2 = new FileStream(fileName2, FileMode.Open))
                {
                    // Compute file hashes
                    fileHash1 = hash.ComputeHash(fileStream1);
                    fileHash2 = hash.ComputeHash(fileStream2);
                }

                return BitConverter.ToString(fileHash1) == BitConverter.ToString(fileHash2);
            }
            else
            {
                return false;
            }
        }

        private static bool CompareFileSizes(string fileName1, string fileName2)
        {
            
            

                bool fileSizeEqual = true;

            if (File.Exists(fileName1) && File.Exists(fileName2))
            {

                // Create System.IO.FileInfo objects for both files
                FileInfo fileInfo1 = new FileInfo(fileName1);
                FileInfo fileInfo2 = new FileInfo(fileName2);

                // Compare file sizes
                if (fileInfo1.Length != fileInfo2.Length)
                {
                    // File sizes are not equal therefore files are not identical
                    fileSizeEqual = false;
                }

            }   
            
            return fileSizeEqual;
        }

        public static bool IsUNCPath(
        string path)
        {
            try { return (new Uri(path)).IsUnc; }
            catch { return false; }
        }

        public static Dictionary<string, string> GetShareUNCPathToPhysicalPathMappings()
        {
            // Create a blank dictionary to hold the mappings.
            Dictionary<string, string> mappings = new Dictionary<string, string>();

            // Get this PC's host name.
            string hostName = Dns.GetHostName();

            // Get the registry key that contains the share information.
            using (RegistryKey shareKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanServer\Shares"))
            {
                // If the registry key isn't null...
                if (shareKey != null)
                {
                    // Get the share names and go through each one...
                    string[] shareNames = shareKey.GetValueNames();
                    foreach (string shareName in shareNames)
                    {
                        // Get the properties for the share and go through each one.
                        string[] shareProperties = (string[])shareKey.GetValue(shareName);
                        foreach (string shareProperty in shareProperties)
                        {
                            // Find the path property for the share and create the mapping.
                            if (shareProperty.StartsWith("Path="))
                            {
                                mappings[string.Format(@"\\{0}\{1}\", hostName, shareName)] = shareProperty.Remove(0, 5) + @"\";
                                break;
                            }
                        }
                    }
                }
            }

            // Return the mappings.
            return mappings;
        }

        public static string ConvertUNCPathToPhysicalPath(
       string uncPath)
        {
            // If the supplied path isn't a UNC path, return null.
            if (!IsUNCPath(uncPath)) return null;

            // Attempt to find the physical path that the UNC path corresponds to.
            Dictionary<string, string> mappings = GetShareUNCPathToPhysicalPathMappings();
            foreach (string shareUNCPath in mappings.Keys)
            {
                if (uncPath.StartsWith(shareUNCPath))
                {
                    return mappings[shareUNCPath] + uncPath.Remove(0, shareUNCPath.Length);
                }
            }

            // If no mapping could be found, return null.
            return null;
        }

    }
}
