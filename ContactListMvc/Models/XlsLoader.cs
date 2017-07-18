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
        private IRepSqlServer _sqlServer;

        public XlsLoader(IRepExcel excel, IRepSqlServer sqlServer)
        {
            _excel = excel;
            _sqlServer = sqlServer;
        }

        public bool LoadToDb(string filename, string comment)
        {
            DataTable dt = _excel.LoadFromFile(filename);
            
            _sqlServer.Bulk(dt);

            return dt.Rows.Count > 0;
        }
    }
}