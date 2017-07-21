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

        public bool LoadToDb(string filename, string origfilename, string comment)
        {
            DataTable dt = _excel.LoadFromFile(filename);

            // на время разработки, потом убрать
            _sqlServer.DropTempTable();

            _sqlServer.DataUploadToDB(dt, origfilename, comment);

            //_sqlServer.InsertIntoLists(origfilename, comment);

            return dt.Rows.Count > 0;
        }
    }
}