using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactListMvc.Models.Repository.InteropExcel
{
    public class Repository
    {
        protected string ServerPath { get; set; }

        public Repository()
        {
            ServerPath = HttpContext.Current.Server.MapPath("~/Files/");
        }
    }
}