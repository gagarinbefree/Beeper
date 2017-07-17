using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactListMvc.Models.Repository.OleDb
{
    public class Repository
    {       
        public string CreateConnectionString(string filename)
        {
            return String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 12.0 XML;HDR=YES;';Data Source={0}{1};",
                HttpContext.Current.Server.MapPath("~/Files/"), filename);
        }
    }
}