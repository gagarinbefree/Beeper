using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactListMvc.Models.ViewModels
{
    public class PersonViewModel
    {
        public int id { set; get; }
        public string lastname { set; get; }
        public string firstname { set; get; }
        public string middlename { set; get; }
        public string sex { set; get; }
        public string birthday { set; get; }
        public string phone { set; get; }
        public string city { set; get; }
        public string category { set; get; }
        public string isvalid { set; get; }
    }
}