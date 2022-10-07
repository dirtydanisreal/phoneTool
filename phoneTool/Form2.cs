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
using Microsoft.VisualBasic;

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

            Form f = Application.OpenForms[0];
            string name = textBox1.Text;
            string phone = textBox2.Text;
            //string alt = textBox3.Text;
            //string pager = textBox4.Text;
            //string fax = textBox5.Text;
            //string tube = textBox6.Text;


            Helper.RemoveChars(phone);
            //Helper.RemoveChars(alt);
            //Helper.RemoveChars(pager);
            //Helper.RemoveChars(fax);
            //Helper.RemoveChars(tube);

            Helper.PhoneNumber(phone);
            //Helper.PhoneNumber(alt);
            //Helper.PhoneNumber(tube);
            //Helper.PhoneNumber(pager);
            //Helper.PhoneNumber(fax);


            //if (Helper.IsOnlyNumbers(phone) || Helper.IsOnlyNumbers(alt) || Helper.IsOnlyNumbers(tube) || Helper.IsOnlyNumbers(pager) || Helper.IsOnlyNumbers(fax) == false)
            //{
            //MessageBox.Show("Please make sure you only input numbers only");
            //return;
            //}



            string newLine = name.Trim() + "," + Helper.PhoneNumber(phone);


            //string path = @"\\ukhcdata\\dept\\Trauma Services\\Trauma Surgical Clerks\\numberData\\numberData.txt";
            string path = @"\\ukhcdata\\dept\\Trauma Services\\Trauma Surgical Clerks\\numberData\\staffNumbers.txt";
            string addLine = newLine + Environment.NewLine;
            File.AppendAllText(path, addLine);

            Application.Restart();
            
            
            
            
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

        private TForm getForm<TForm>()
        where TForm : Form
        {
            return (TForm)Application.OpenForms.OfType<TForm>().FirstOrDefault();
        }


    }
}
