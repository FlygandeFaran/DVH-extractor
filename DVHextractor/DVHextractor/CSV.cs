using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Data;

namespace DVHextractor
{
    class CSV
    {
        public void WriteToCSV(string fileName, List<string> strOut)
        {
            StreamWriter sw = new StreamWriter(fileName, false);
            foreach (string text in strOut)
            {
                sw.Write(text);
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
        public List<string> ReadFromCSV(string fileName)
        {
            List<string> strOut = new List<string>();
            StreamReader sr = new StreamReader(fileName, false);
            while(!sr.EndOfStream)
            {
                strOut.Add(sr.ReadLine());
            }
            sr.Close();
            return strOut;
        }
        public void ExportToExcel(DataTable submittedDataTable, string filePath, bool isAbs)
        {
            int i = 0;
            StreamWriter sw = null;

            sw = new StreamWriter(filePath, false);

            for (i = 0; i < submittedDataTable.Columns.Count - 1; i++)
            {
                if (isAbs && i < 5 && i > 1)
                    sw.Write(submittedDataTable.Columns[i].ColumnName + "[Gy]" + ";");
                else if (!isAbs && i < 5 && i > 1)
                    sw.Write(submittedDataTable.Columns[i].ColumnName + "[%]" + ";");
                else
                    sw.Write(submittedDataTable.Columns[i].ColumnName + ";");
            }
            sw.Write(submittedDataTable.Columns[i].ColumnName);
            sw.WriteLine();

            foreach (DataRow row in submittedDataTable.Rows)
            {
                object[] array = row.ItemArray;
                string output = "";
                for (i = 0; i < array.Length - 1; i++)
                {
                    output = removeUplanFromNumber(array[i].ToString());
                    sw.Write(output + ";");
                }
                sw.Write(output);
                sw.WriteLine();

            }

            sw.Close();
        }
        private string removeUplanFromNumber(string cellName)
        {
            if (cellName != "")
            {
                if (cellName.Substring(cellName.Length - 1) == ")")
                {
                    while (cellName.Substring(cellName.Length - 1) != "(")
                        cellName = cellName.Remove(cellName.Length - 1);
                    cellName = cellName.Remove(cellName.Length - 2);
                }
            }
            return cellName;
        }
    }
}
