using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using System.Data;
using System.IO;

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
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                res.Rows.Add(newRow);
            }

            return res;
        }
    }
}