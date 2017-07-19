using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactListMvc.Models.Repository.SqlServer
{
    public class RepSqlServer : Repository, IRepSqlServer
    {        
        public void Bulk(System.Data.DataTable dt)
        {
            string table = "templist";

            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {                
                var bulk = new SqlBulkCopy(connection);
                bulk.DestinationTableName = table;
                connection.Open();

                string query = LoadSqlFile("CreateTempTable.sql");
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                var schema = connection.GetSchema("Columns", new[] { null, null, table, null });
                foreach (DataColumn sourceColumn in dt.Columns)
                {
                    foreach (DataRow row in schema.Rows)
                    {
                        if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], StringComparison.OrdinalIgnoreCase))
                        {
                            bulk.ColumnMappings.Add(sourceColumn.ColumnName, (string)row["COLUMN_NAME"]);
                            
                            break;
                        }
                    }
                }
                
                bulk.WriteToServer(dt);
            }
        }

        public void DropTempTable()
        {
            string query = LoadSqlFile("DropTempTable.sql");

            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}