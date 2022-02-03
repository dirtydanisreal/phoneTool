using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Reflection;


namespace phoneTool
{
    public partial class Form1 : Form
    {

        
        public Form1()
        {
            InitializeComponent();
            //WebClient Client = new WebClient();
            //Client.DownloadFile("https://phonenumberdata.s3.us-east-2.amazonaws.com/numberData.dat", "numberData.dat");

        
            string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\phoneTool");
            string filePath = Path.Combine(dataPath, "numberData.csv");
            string networkPath = @"\\ukhcdata\\dept\\Trauma Services\\Trauma Surgical Clerks\\numberData";
            string netPath = ConvertUNCPathToPhysicalPath(networkPath);
            string fileNetPath = Path.Combine(networkPath, "numberData.txt");
            string appPath = Application.StartupPath;
            string appTxt = Path.Combine(dataPath, "numberData.csv");

            //dataGridView1.DataSource = Helper.DataTableFromTextFile(fileNetPath);
            dataGridView1.DataSource = Helper.DataTableFromTextFile(appTxt);

           
            
            dataGridView1.AutoSize = false;
            //foreach (DataGridViewColumn item in dataGridView1.Columns)
            //{
             //   item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    item.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
           // }
            
            txtSearch.AutoSize = false;
            txtSearch.Size = new System.Drawing.Size(500, 26);
            txtSearch.Text.Trim();

            watchFile();
            

            

            
            
            
        }

        public async void backgroundRefresh()
        {
            Task t = updateDataGrid();
            await t;
        }

        public async void watchFile()
        {
            string networkPath = @"\\ukhcdata\\dept\\Trauma Services\\Trauma Surgical Clerks\\numberData";
            string appTxt = Path.Combine(networkPath, "numberData.txt");
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = appTxt;
            watcher.Filter = "*.txt";
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.EnableRaisingEvents = true;

            watcher.Changed += new FileSystemEventHandler(onChange);
        }

        void onChange(object sender, FileSystemEventArgs e)
        {
            backgroundRefresh();
        }

        private async Task updateDataGrid()
        {
            (dataGridView1.DataSource as DataGridView).Refresh();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            
            

            
            
            


        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {

            //string word = txtSearch.Text;
            //if (word.Length == 0) return;

            // Find the best matches.
            //string[] words;
            //int[] values;
            //FindBestMatches(word, 10, out words, out values);

            // Display the best matches.
            //for (int i = 0; i < words.Length; i++)
            //{
            //  txtSearch.Text = (values[i].ToString() +
            //    '\t' + words[i]);
            //}


            Task t = update();
            await t;

        }

        private async Task update()
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", txtSearch.Text.ToUpper());
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
