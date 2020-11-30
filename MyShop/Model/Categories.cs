using System;
using System.Data;
using System.Data.SqlClient;

namespace MyShop.Model
{
    class Categories : DataSelector
    {
        public Categories()
        {
            Table = new DataTable();
        }

        public Categories(SqlConnection connection)
        {
            Table = new DataTable();
            CreateTable(connection);
        }

        private void CreateTable(SqlConnection connection)
        {
            try
            {
                connection.Open();

                var query = @"Create table Categories  
                        ( [CategoryID] int identity(1,1) primary key,
                        [CategoryName] nvarchar(50) not null,
                        [Description] nvarchar(max) not null )";

                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();

                FillTable(connection);
                connection.Close();

                SelectFromTable("Select * from Categories", connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void FillTable(SqlConnection connection)
        {
            var query = @"Insert into Categories 
                        values 
                        ('Beverages', 'Soft drinks, coffees, teas, beers, and ales'),
                        ('Condiments', 'Sweet and savory sauces, relishes, spreads, and seasonings'),
                        ('Confections', 'Desserts, candies, and sweet breads'),
                        ('Dairy Products', 'Cheeses'),
                        ('Grains/Cereals', 'Breads, crackers, pasta, and cereal')";

            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void InsertIntoTable(string categoryName, string description)
        {
            if (string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(description))
            {
                return;
            }

            var query = $"Insert into Categories (CategoryName, Description) values (@name, @desc)";

            try
            {
                DataManager.Manager.Connection.Open();
                var command = new SqlCommand(query, DataManager.Manager.Connection);
                var nameParam = new SqlParameter("@name", categoryName);
                var descParam = new SqlParameter("@desc", description);

                command.Parameters.AddRange(new[] { nameParam, descParam });
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

            var query = $"Select Top(1) CategoryID from Categories where CategoryName = @name";
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
