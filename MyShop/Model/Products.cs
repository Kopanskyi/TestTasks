using System;
using System.Data;
using System.Data.SqlClient;

namespace MyShop.Model
{
    class Products : DataSelector
    {
        public Products()
        {
            Table = new DataTable();
        }

        public Products(SqlConnection connection)
        {
            Table = new DataTable();
            CreateTable(connection);
        }

        private void CreateTable(SqlConnection connection)
        {
            try
            {
                connection.Open();

                var query = @"Create table Products
                            ( [ProductID] int identity(1,1) primary key,
                            [ProductName] nvarchar(50) not null,
                            [SupplierID] int foreign key references Suppliers(SupplierID),
                            [CategoryID] int foreign key references Categories(CategoryID),
                            [Price] money not null )";

                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();

                FillTable(connection);
                connection.Close();

                SelectFromTable("Select * from Products", connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void FillTable(SqlConnection connection)
        {
            var query = @"Insert into Products 
                        values 
                        ('Chais', 1, 1, 18.00),
                        ('Chang', 1, 1, 19.00),
                        ('Aniseed Syrup', 1, 2, 10.00),
                        ('Chef Anton’s Cajun Seasoning', 2, 2, 22.00),
                        ('Chef Anton’s Gumbo Mix', 2, 2, 21.35)";

            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void InsertIntoTable(string productName, string supplierName, string categoryName, decimal price)
        {
            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(supplierName) || 
                string.IsNullOrEmpty(categoryName) || price <= 0)
            {
                return;
            }

            var query = @"Insert into Products values (@name, @supplier, @category, @price)";

            try
            {
                var supplierID = DataManager.Manager.Suppliers.SelectIDbyName(supplierName);
                var categoryID = DataManager.Manager.Categories.SelectIDbyName(categoryName);

                if (supplierID == 0 || categoryID == 0)
                {
                    throw new Exception("Can't find supplier or category with these names");
                }

                DataManager.Manager.Connection.Open();
                var command = new SqlCommand(query, DataManager.Manager.Connection);
                var nameParam = new SqlParameter("@name", productName);
                var supplierParam = new SqlParameter("@supplier", supplierID);
                var categoryParam = new SqlParameter("@category", categoryID);
                var priceParam = new SqlParameter("@price", price);

                command.Parameters.AddRange(new[] { nameParam, supplierParam, categoryParam, priceParam });
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

            var query = $"Select Top(1) ProductID from Products where ProductName = @name";
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
