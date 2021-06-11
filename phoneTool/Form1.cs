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

            dataGridView1.DataSource = Helper.DataTableFromTextFile(filePath);
            dataGridView1.AutoSize = false;
            //foreach (DataGridViewColumn item in dataGridView1.Columns)
            //{
             //   item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    item.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
           // }
            
            txtSearch.AutoSize = false;
            txtSearch.Size = new System.Drawing.Size(500, 26);
            txtSearch.Text.Trim();
            

            

            
            
            
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
    }
}
