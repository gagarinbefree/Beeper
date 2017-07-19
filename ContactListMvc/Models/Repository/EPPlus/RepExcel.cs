using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace ContactListMvc.Models.Repository.EPPlus
{
    public class RepExcel : Repository, IRepExcel
    {
        public System.Data.DataTable LoadFromFile(string filename)
        {
            DataTable res = new DataTable();

            FileInfo file = new FileInfo(String.Format("{0}{1}", ServerPath, filename));
            ExcelPackage excel = new ExcelPackage(file);
            ExcelWorksheet workSheet = excel.Workbook.Worksheets.First();

            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                res.Columns.Add(firstRowCell.Text);
            }

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                var newRow = res.NewRow();

                var cells = row.Value as object[,];
                if (cells == null)
                    continue;

                for (var cellnumber = 0; cellnumber < cells.GetLength(1); cellnumber ++)
                { 
                    newRow[cellnumber] = _getCellValue(cellnumber, cells[0, cellnumber]);
                }

                res.Rows.Add(newRow);
            }

            return res;
        }

        private string _getCellValue(int cellnumber, object cell)
        {
            if (cell == null)
                return "";            
            else if (cellnumber == 0)
                return _phone(cell);
            else if (cellnumber == 6)
                return _city(cell);
            else if (cellnumber == 7)
                return _birthday(cell);
            else
                return cell.ToString();
        }

        private string _phone(object cell)
        {
            Regex rgx = new Regex("[^0-9]");

            return rgx.Replace(cell.ToString(), "");
        }

        private string _city(object cell)
        {
            Regex rgx = new Regex(@"(^\W*)|(\W*$)");
            string city = rgx.Replace(cell.ToString(), "");

            return city.Length > 1 ? city : "не определен";
        }

        private string _birthday(object cell)
        {            
            DateTime dt = DateTime.MinValue;
            DateTime.TryParse(cell.ToString(), out dt);

            return dt.ToString("dd.MM.yyyy");
        }
    }
}