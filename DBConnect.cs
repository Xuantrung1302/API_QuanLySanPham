using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjectDataAPI
{
    public class DBConnect
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;


        public DBConnect()
        {

        }

        // Thực thi stored procedure mà không trả về dữ liệu
        public bool ExecuteNonQuery(string procedureName, SqlParameter[] parameters)
        {
            bool result = true;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(parameters);
                        connection.Open();
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        // Thực thi stored procedure và trả về dữ liệu
        public DataTable ExecuteQuery(string procedureName, SqlParameter[] parameters = null)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }
    }
}