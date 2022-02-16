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

namespace phoneTool
{

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string name  = format trim ( textBox1.Text)
            //string phone = cccc - c (textBox2.Text)
            string name = textBox1.Text;
            string phone = textBox2.Text;
            string alt = textBox3.Text;
            string pager = textBox4.Text;
            string fax = textBox5.Text;
            string tube = textBox6.Text;
            if (phone.Length < 5)
            {

            }

            string newLine = name.Trim() + "," + phone.Trim() + "," +alt.Trim() + "," + pager.Trim() + "," + fax.Trim() + "," + tube.Trim();
            string path = @"D:\GitHub\phoneTool\phoneTool\bin\Debug\numberData.txt";
            string addLine = newLine + Environment.NewLine;
            File.AppendAllText(path, addLine);

            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            
        }
    }
}
