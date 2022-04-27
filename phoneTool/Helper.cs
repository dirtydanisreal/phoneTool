using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.Globalization;

namespace phoneTool
{
    public static class Helper
    {
        public static DataTable DataTableFromTextFile(string location, char delimiter = ',')
        {
                DataTable result;

                string[] LineArray = File.ReadAllLines(location);
                
                result = FormDataTable(LineArray, delimiter);

                return result; 
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteFile(string name);


        private static DataTable FormDataTable(string[] LineArray, char delimiter)
        {
           

            DataTable dt = new DataTable();

            AddColumnToTable(LineArray, delimiter, ref dt);

            AddRowToTable(LineArray, delimiter, ref dt);

            return dt;
        }


        private static void AddRowToTable(string[] valueCollection, char delimiter, ref DataTable dt)
        {

            for (int i = 1; i < valueCollection.Length; i++)
            {
                string[] values = valueCollection[i].Split(delimiter);

                DataRow dr = dt.NewRow();

                for (int j = 0; j < values.Length; j++)
                {
                    dr[j] = values[j];
                }

                dt.Rows.Add(dr);
            }
        }


        private static void AddColumnToTable(string[] columnCollection, char delimiter, ref DataTable dt)
        {
            string[] columns = columnCollection[0].Split(delimiter);

            foreach (string columnName in columns)
            {
                DataColumn dc = new DataColumn(columnName, typeof(string));
                dt.Columns.Add(dc);
            }
        }

        public static async Task AppendLineToFileAsync( string path, string line)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentOutOfRangeException(nameof(path), path, "Was null or whitepsace.");

            if (!File.Exists(path))
                throw new FileNotFoundException("File not found.", nameof(path));

            using (var file = File.Open(path, FileMode.Append, FileAccess.Write))
            using (var writer = new StreamWriter(file))
            {
                await writer.WriteLineAsync(line);
                await writer.FlushAsync();
            }
        }

        public static void formatNumber(string Number)
        {
            bool result = IsInteger(Number);
            int pLength = Number.Length;

            

            Number.Trim();
            Number.Replace(" ", "");
            if ((pLength < 5) && (result == true))
            {
                int index1 = Number.IndexOf("-");
                if (Number.Contains("-") && (index1 != 2))
                {
                    Number.Replace("-", "");
                    Number.Insert(2, "-");
                }
            }
            else if ((pLength > 5) && (pLength <= 7) && (result == true))
            {
                Number.Replace("-", "");
                Number.Insert(4, "-");
            }
            else if ((pLength > 7) && (result == true))
            {
                Number.Replace("(", "");
                Number.Replace(")", "");
                Number.Replace("-", "");

                Number.Insert(7, "-");
                Number.Insert(4, ")-");
                Number.Insert(1, "(");

            }

            
        }

        public static bool IsInteger(this string val)
        {
            int retNum;

            bool isNum = Int32.TryParse(val, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static string PhoneNumber(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            value = new System.Text.RegularExpressions.Regex(@"\D")
                .Replace(value, string.Empty);
            value = value.TrimStart('0');
            if (value.Length == 5)
                return Convert.ToInt64(value).ToString("# - ####");
            if (value.Length < 5 || value.Length <= 7)
                return Convert.ToInt64(value).ToString("###-####");
            if (value.Length > 7)
                return Convert.ToInt64(value).ToString("(###) ###-####");
            if (value.Length == 3)
                return Convert.ToInt64(value).ToString("###");
            
            return value;
        }
        
        public static bool isNumeric(string s)
        {
        foreach (char c in s)
            {
                if (!(c >= '0' && c <= '9')) 
                {
                return false;
                }
            }
 
            return true;
        }


    }

       


}
