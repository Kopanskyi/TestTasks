using System;
using System.Data;
using System.Data.SqlClient;

namespace MyShop.Model
{
    class Suppliers : DataSelector
    {
        public Suppliers()
        {
            Table = new DataTable();
        }

        public Suppliers(SqlConnection connection)
        {
            Table = new DataTable();
            CreateTable(connection);
        }

        private void CreateTable(SqlConnection connection)
        {
            try
            {
                connection.Open();

                var query = @"Create table Suppliers
                            ( [SupplierID] int identity(1,1) primary key,
                            [SupplierName] nvarchar(50) not null,
                            [City] nvarchar(50) not null,
                            [Country] nvarchar(50) not null )";

                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();

                FillTable(connection);
                connection.Close();

                SelectFromTable("Select * from Suppliers", connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FillTable(SqlConnection connection)
        {
            var query = @"Insert into Suppliers 
                        values 
                        ('Exotic Liquid', 'London', 'UK'),
                        ('New Orleans Cajun Delights', 'New Orleans', 'USA'),
                        ('Grandma Kelly’s Homestead', 'Ann Arbor', 'USA'),
                        ('Tokyo Traders', 'Tokyo', 'Japan'),
                        ('Cooperativa de Quesos ‘Las Cabras’', 'Oviedo', 'Spain')";

            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void InsertIntoTable(string supplierName, string city, string country)
        {
            if (string.IsNullOrEmpty(supplierName) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
            {
                return;
            }

            var query = $"Insert into Suppliers values (@name, @city, @country)";

            try
            {
                DataManager.Manager.Connection.Open();
                var command = new SqlCommand(query, DataManager.Manager.Connection);
                var nameParam = new SqlParameter("@name", supplierName);
                var cityParam = new SqlParameter("@city", city);
                var countryParam = new SqlParameter("@country", country);

                command.Parameters.AddRange(new[] { nameParam, cityParam, countryParam });
                query = command.CommandText;
                command.ExecuteNonQuery();
                DataManager.Manager.Connection.Close();
                Console.WriteLine($"Query: {query} \nExecuted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public int SelectIDbyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 0;
            }

            var query = $"Select Top(1) SupplierID from Suppliers where SupplierName = @name";
            var result = new object();

            try
            {
                DataManager.Manager.Connection.Open();
                var command = new SqlCommand(query, DataManager.Manager.Connection);
                var nameParam = new SqlParameter("@name", name);
                command.Parameters.Add(nameParam);

                result = command.ExecuteScalar();
                DataManager.Manager.Connection.Close();

                if (result == null)
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return (int) result;
        }
    }
}
