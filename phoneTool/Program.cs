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
            
            Application.Run(new Form1());
            
            
            

        }  

    }
}
