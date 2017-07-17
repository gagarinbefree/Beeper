using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ContactListMvc.Models.Repository
{
    public interface IRepExcel
    {
        DataTable LoadFromFile(string filename);
    }
}
