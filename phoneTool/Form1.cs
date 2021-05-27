using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Reflection;
using AutoUpdaterDotNET;

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

        private string[] Words;

        private enum Direction
        {
            FromAbove,
            FromLeft,
            FromDiagonal
        }

        private struct Node
        {
            public int distance;
            public Direction direction;
        }

        private Node[,] MakeEditGraph(string string1, string string2)
        {
            // Make the edit graph array.
            int num_cols = string1.Length + 1;
            int num_rows = string2.Length + 1;
            Node[,] nodes = new Node[num_rows, num_cols];

            // Initialize the leftmost column.
            for (int r = 0; r < num_rows; r++)
            {
                nodes[r, 0].distance = r;
                nodes[r, 0].direction = Direction.FromAbove;
            }

            // Initialize the top row.
            for (int c = 0; c < num_cols; c++)
            {
                nodes[0, c].distance = c;
                nodes[0, c].direction = Direction.FromLeft;
            }

            // Fill in the rest of the array.
            char[] chars1 = string1.ToCharArray();
            char[] chars2 = string2.ToCharArray();
            for (int c = 1; c < num_cols; c++)
            {
                // Fill in column c.
                for (int r = 1; r < num_rows; r++)
                {
                    // Fill in entry [r, c].
                    // Check the three possible paths to here.
                    int distance1 = nodes[r - 1, c].distance + 1;
                    int distance2 = nodes[r, c - 1].distance + 1;
                    int distance3 = int.MaxValue;
                    if (chars1[c - 1] == chars2[r - 1])
                    {
                        // There is a diagonal link.
                        distance3 = nodes[r - 1, c - 1].distance;
                    }

                    // See which is cheapest.
                    if ((distance1 <= distance2) && (distance1 <= distance3))
                    {
                        // Come from above.
                        nodes[r, c].distance = distance1;
                        nodes[r, c].direction = Direction.FromAbove;
                    }
                    else if (distance2 <= distance3)
                    {
                        // Come from the left.
                        nodes[r, c].distance = distance2;
                        nodes[r, c].direction = Direction.FromLeft;
                    }
                    else
                    {
                        // Come from the diagonal.
                        nodes[r, c].distance = distance3;
                        nodes[r, c].direction = Direction.FromDiagonal;
                    }
                }
            }

            // Display the graph's nodes (for debugging).
            //DumpArray(nodes);

            return nodes;
        }

        // Display the graph's nodes (for debugging).
        private void DumpArray(Node[,] nodes)
        {
            int num_rows = nodes.GetUpperBound(0) + 1;
            int num_cols = nodes.GetUpperBound(1) + 1;

            Console.WriteLine("**********");
            // Fill in column c.
            for (int r = 0; r < num_rows; r++)
            {
                for (int c = 0; c < num_cols; c++)
                {
                    string txt = "";
                    switch (nodes[r, c].direction)
                    {
                        case Direction.FromAbove:
                            txt = "v";
                            break;
                        case Direction.FromLeft:
                            txt = "-";
                            break;
                        case Direction.FromDiagonal:
                            txt = "\\";
                            break;
                    }
                    txt += nodes[r, c].distance.ToString();
                    Console.Write(string.Format("{0,3}", txt));
                }
                Console.WriteLine();
            }
            Console.WriteLine("**********");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start("https://phonenumberdata.s3.us-east-2.amazonaws.com/update.xml");
            AutoUpdater.Synchronous = true;
            AutoUpdater.Mandatory = true;
            AutoUpdater.UpdateMode = Mode.Forced;
            
            
            

            
            Words = File.ReadAllLines("numberData.dat");
            txtSearch.Text = "";
            AutoCompleteStringCollection word_source =
                new AutoCompleteStringCollection();
            word_source.AddRange(Words);
            txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearch.AutoCompleteCustomSource = word_source;
            txtSearch.AutoCompleteMode = AutoCompleteMode.Suggest;


        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
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

           
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", txtSearch.Text.ToUpper());
           
        }

        private void FindBestMatches(string word, int num_matches, out string[] words, out int[] values)
        {
            // Find words that start with the same letter.
            string start_char = word.Substring(0, 1).ToUpper();
            int start_index = Array.BinarySearch(Words, start_char);
            List<string> match_words = new List<string>();
            List<int> match_values = new List<int>();
            for (int i = start_index + 1; i < Words.Length; i++)
            {
                // Get the next word and make sure it starts with the same letter.
                string test_word = Words[i];
                if (test_word.Substring(0, 1).ToUpper() != start_char) break;

                // Consider the next word up to the length of the typed word.
                int max_length = Math.Min(test_word.Length, word.Length);
                string short_word = test_word.Substring(0, max_length);

                // Build the edit graph.
                Node[,] nodes = MakeEditGraph(word, short_word);

                // List the distance.
                int x = nodes.GetUpperBound(0);
                int y = nodes.GetUpperBound(1);
                match_words.Add(test_word);
                match_values.Add(nodes[x, y].distance);
            }

            // Sort the matches by distance, smallest distance first.
            string[] match_words_array = match_words.ToArray();
            int[] match_values_array = match_values.ToArray();
            Array.Sort(match_values_array, match_words_array);

            // Return the desired number of matches.
            int max = Math.Min(num_matches, match_values_array.Length);
            words = new string[max];
            Array.Copy(match_words_array, words, max);
            values = new int[max];
            Array.Copy(match_values_array, values, max);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
