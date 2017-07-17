using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using ExcelObj = Microsoft.Office.Interop.Excel;
using System.Data;

namespace ContactListMvc.Models.Repository.InteropExcel
{
    public class RepExcel : Repository, IRepExcel
    {
        // медлено
        public DataTable Old_LoadFromFile(string filename)
        {            
            DataTable res = new DataTable();

            ExcelObj.Application app = new ExcelObj.Application();
            ExcelObj.Workbook workbook;
            ExcelObj.Worksheet sheet;
            ExcelObj.Range range;

            string path = HttpContext.Current.Server.MapPath(String.Format("{0}/{1}", ServerPath, filename));

            workbook = app.Workbooks.Open(path, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value);

            sheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);
            range = sheet.UsedRange;

            for (int cc = 1; cc <= range.Columns.Count; cc++)
            {
                res.Columns.Add(
                   new DataColumn((range.Cells[1, cc] as ExcelObj.Range).Value2.ToString()));
            }
            res.AcceptChanges();

            string[] columnNames = new String[res.Columns.Count];
            for (int ii = 0; ii < res.Columns.Count; ii++)
            {
                columnNames[0] = res.Columns[ii].ColumnName;
            }              
            
            for (int rr = 2; rr <= range.Rows.Count; rr++)
            {
                DataRow dr = res.NewRow();
                for (int cc = 1; cc <= range.Columns.Count; cc++)
                {
                    if ((range.Cells[rr, cc] as ExcelObj.Range).Value2 != null)
                        dr[cc - 1] = (range.Cells[rr, cc] as ExcelObj.Range).Value2.ToString();
                }
                
                res.Rows.Add(dr);                
            }
            res.AcceptChanges();

            return res;
        }

        public DataTable LoadFromFile(string filename)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            
            xlApp = new Microsoft.Office.Interop.Excel.Application();

            string path = String.Format("{0}/{1}", ServerPath, filename);

            xlWorkBook = xlApp.Workbooks.Open(path, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value);


            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            return GetWorksheetAsDataTable(xlWorkSheet);
        }

        public DataTable GetWorksheetAsDataTable(Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            var dt = new DataTable(worksheet.Name);
            dt.Columns.AddRange(GetDataColumns(worksheet).ToArray());
            var headerOffset = 1; //have to skip header row
            var width = dt.Columns.Count;
            var depth = GetTableDepth(worksheet, headerOffset);
            for (var i = 1; i <= depth; i++)
            {
                var row = dt.NewRow();
                for (var j = 1; j <= width; j++)
                {
                    var currentValue = worksheet.Cells[i + headerOffset, j].Value;

                    //have to decrement b/c excel is 1 based and datatable is 0 based.
                    row[j - 1] = currentValue == null ? null : currentValue.ToString();
                }

                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// Assumption: There are no null or empty cells in the first column
        /// </summary>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        private int GetTableDepth(Microsoft.Office.Interop.Excel.Worksheet worksheet, int headerOffset)
        {
            var i = 1;
            var j = 1;
            var cellValue = worksheet.Cells[i + headerOffset, j].Value;
            while (cellValue != null)
            {
                i++;
                cellValue = worksheet.Cells[i + headerOffset, j].Value;
            }

            return i - 1; //subtract one because we're going from rownumber (1 based) to depth (0 based)
        }

        private IEnumerable<DataColumn> GetDataColumns(Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            return GatherColumnNames(worksheet).Select(x => new DataColumn(x));
        }

        private IEnumerable<string> GatherColumnNames(Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            var columns = new List<string>();

            var i = 1;
            var j = 1;
            var columnName = worksheet.Cells[i, j].Value;
            while (columnName != null)
            {
                columns.Add(GetUniqueColumnName(columns, columnName.ToString()));
                j++;
                columnName = worksheet.Cells[i, j].Value;
            }

            return columns;
        }

        private string GetUniqueColumnName(IEnumerable<string> columnNames, string columnName)
        {
            var colName = columnName;
            var i = 1;
            while (columnNames.Contains(colName))
            {
                colName = columnName + i.ToString();
                i++;
            }

            return colName;
        }
    }
}