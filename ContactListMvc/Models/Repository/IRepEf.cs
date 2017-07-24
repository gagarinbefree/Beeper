using ContactListMvc.Models.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactListMvc.Models.Repository
{
    public interface IRepEf
    {
        List<persons> GetPersons(int? page, int? limit, string sortBy, string direction);
    }
}
