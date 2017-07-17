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
        public DataTable LoadFromFile(string filename)
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
    }
}