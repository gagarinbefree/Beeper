using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ContactListMvc.Models.Repository;

namespace ContactListMvc.Models
{
    public class XlsLoader : ILoader
    {
        private IRepExcel _excel;

        public XlsLoader(IRepExcel excel)
        {
            _excel = excel;
        }

        public bool LoadToDb(string filename, string comment)
        {
            DataTable dt = _excel.LoadFromFile(filename);

            return dt.Rows.Count > 0;
        }
    }
}