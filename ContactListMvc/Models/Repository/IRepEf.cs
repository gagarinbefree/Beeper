using ContactListMvc.Models.Repository.EF;
using ContactListMvc.Models.Repository.EF.DTO;
using ContactListMvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactListMvc.Models.Repository
{
    public interface IRepEf
    {
        ContactListViewModel GetPersons(int? page, int? limit, string sortBy, string direction);
    }
}
