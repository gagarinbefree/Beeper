using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactListMvc.Models
{
    public interface ILoader
    {
        bool LoadToDb(string filename, string comment);
    }
}
