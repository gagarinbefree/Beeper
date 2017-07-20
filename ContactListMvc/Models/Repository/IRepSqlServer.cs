using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactListMvc.Models.Repository
{
    public interface IRepSqlServer
    {        
        void DataUploadToDB(DataTable dt);

        void DropTempTable();
    }
}
