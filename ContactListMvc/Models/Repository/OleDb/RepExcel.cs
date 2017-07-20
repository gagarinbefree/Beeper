using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

namespace ContactListMvc.Models.Repository.OleDb
{
    public class RepExcel : Repository, IRepExcel
    {
        // требует установленого провайдера OleDb
        public System.Data.DataTable LoadFromFile(string filename)
        {
            OleDbConnection con = new OleDbConnection(CreateConnectionString(filename));
            
            con.Open();

            DataSet ds = new DataSet();
            DataTable schemaTable = con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables,
                new object[] { null, null, null, "TABLE" });

            string sheet1 = (string)schemaTable.Rows[0].ItemArray[2];
            string select = String.Format("SELECT * FROM [{0}]", sheet1);

            System.Data.OleDb.OleDbDataAdapter ad =
                new System.Data.OleDb.OleDbDataAdapter(select, con);

            ad.Fill(ds);

            DataTable tb = ds.Tables[0];
            con.Close();

            return tb;
        }

        public DataTable LoadFromPartFile(string filename, int page, int pagesize)
        {
            throw new NotImplementedException();
        }
    }
}