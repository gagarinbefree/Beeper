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
                res.Columns.Add();
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
                    //if (cells[0, cellnumber] != null && cells[0, cellnumber].ToString().IndexOf("Афонин") >= 0) { }

                    newRow[cellnumber] = _getCellValue(cellnumber, cells[0, cellnumber]);
                }

                res.Rows.Add(newRow);
            }

            return res;
        }

        private string _getCellValue(int cellnumber, object cell)
        {
            switch (cellnumber)
            {
                case 0:
                    return _phone(cell);
                case 6:
                    return _city(cell);
                case 7:
                    return _birthday(cell);
                default:
                    return _plain(cell);
            }            
        }

        private string _plain(object cell)
        {
            return cell != null ? cell.ToString() : null;
        }

        private string _phone(object cell)
        {
            if (cell == null)
                return null;

            Regex rgx = new Regex("[^0-9]");

            return rgx.Replace(cell.ToString(), "");
        }

        private string _city(object cell)
        {
            if (cell == null)
                return "Не определен";
                
            Regex rgx = new Regex(@"(^\W*)|(\W*$)");
            
            string city = rgx.Replace(cell.ToString(), "");

            return city.Length > 1 ? city : "Не определен";
        }

        private string _birthday(object cell)
        {
            if (cell == null)
                return null;

            DateTime dt;
            
            return DateTime.TryParse(cell.ToString(), out dt) ? dt.ToString("dd.MM.yyyy") : null;
        }
    }
}