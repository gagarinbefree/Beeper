using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace ContactListMvc.Models.Repository.SqlServer
{
    public class Repository
    {
        protected string ConnectionString;

        public Repository()
        {
            ConnectionString = _createConnectionString();
        }

        private string _createConnectionString()
        {
            string template = ConfigurationManager
                .ConnectionStrings["SqlServerConnection"]
                .ConnectionString;

            string path = String.Format("{0}beeper.mdf"
                , HttpContext.Current.Server.MapPath("~/App_Data/"));

            return String.Format(template, path);
        }

        protected string LoadSqlFile(string fileName)
        {
            string ret;

            string fullName = String.Format("{0}\\{1}", HttpContext.Current.Server.MapPath("~/App_Data"), fileName);
            using (StreamReader sr = new StreamReader(fullName))
            {
                ret = sr.ReadToEnd();
            }

            return ret;
        }
    }
}
