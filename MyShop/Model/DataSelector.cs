using System;
using System.Data;
using System.Data.SqlClient;

namespace MyShop.Model
{
    abstract class DataSelector
    {
        public DataTable Table { get; set; }

        public virtual DataTable SelectFromTable(string query, SqlConnection connect = null)
        {
            if (string.IsNullOrEmpty(query))
            {
                return null;
            }

            var connection = connect;

            if (connection == null)
            {
                connection = DataManager.Manager.Connection;
            }

            try
            {
                connection.Open();

                var adapter = new SqlDataAdapter(query, connection);
                var dataSet = new DataSet();
                dataSet.EnforceConstraints = false;
                adapter.Fill(dataSet);

                Table.Dispose();
                Table = dataSet.Tables[0];

                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                Console.WriteLine(ex.Message);
            }

            return Table;
        }
    }
}
