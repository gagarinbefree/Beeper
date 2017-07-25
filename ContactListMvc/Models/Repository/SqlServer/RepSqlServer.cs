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
        private string _tablename = "#templist";

        public void DataUploadToDB(DataTable data, string file, string comment)
        {            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                _createTempTable(connection);

                _bulk(connection, data);

                _transfer(connection, file, comment);
            }
        }

        private void _createTempTable(SqlConnection connection)
        {
            string query = LoadSqlFile("CreateTempTable.sql");
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void _bulk(SqlConnection connection, DataTable data)
        {
            var bulk = new SqlBulkCopy(connection);
            bulk.DestinationTableName = _tablename;            

            var schema = connection.GetSchema("Columns", new[] { null, null, _tablename, null });
            foreach (DataColumn sourceColumn in data.Columns)
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

            bulk.WriteToServer(data);
        }

        private void _transfer(SqlConnection connection, string file, string comment)
        {
            string query = LoadSqlFile("TransferFromTempTable.sql");

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@file", file);
                cmd.Parameters.AddWithValue("@comment", comment);

                cmd.ExecuteNonQuery();
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
       
        public void InsertIntoLists(string file, string comment)
        {
            string query = LoadSqlFile("InsertIntoLists.sql");

            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@file", file);
                    cmd.Parameters.AddWithValue("@comment", comment);

                    cmd.ExecuteNonQuery();
                }
            }       
        }
    }
}
