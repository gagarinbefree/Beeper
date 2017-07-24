using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactListMvc.Models.ViewModels
{
    public class ContactListViewModel
    {
        public List<PersonViewModel> records { set; get; }
    
        public int total { set; get; }

        public ContactListViewModel()
        {
            records = new List<PersonViewModel>();
        }
    }
}