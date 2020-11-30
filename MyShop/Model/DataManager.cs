using System;
using System.Data.SqlClient;

namespace MyShop.Model
{
    class DataManager
    {
        private string connectWithoutDBexisting 
            = @"data source=HAMANN\HAMANN;database=master;user id=sa;password=1;persist security info=True;Pooling=false;";
        private string connectSt 
            = @"data source=HAMANN\HAMANN;database=MyShop;user id=sa;password=1;persist security info=True;Pooling=false;";
        private static DataManager manager;

        public SqlConnection Connection { get; set; }
        public Categories Categories { get; set; }
        public Suppliers Suppliers { get; set; }
        public Products Products { get; set; }

        public static DataManager Manager
        {
            get
            {
                if (manager == null)
                {
                    manager = new DataManager();
                }

                return manager;
            }
        }

        private DataManager()
        {
            var dbExists = false;
            Connection = new SqlConnection(connectSt);

            try
            {
                Connection.Open();
                Suppliers = new Suppliers();
                Categories = new Categories();
                Products = new Products();

            }
            catch (SqlException)
            {
                Connection.Close();
                CreateDB();
                dbExists = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wrong connection! {ex.Message}");
            }
            finally
            {
                Connection.Close();

                if (dbExists)
                {
                    CreateInstances();
                }
            }
        }

        private void CreateDB()
        {
            Connection = new SqlConnection(connectWithoutDBexisting);

            try
            {
                Connection.Open();
                var query = @"CREATE DATABASE MyShop ON PRIMARY 
                            (NAME = MyShop_Data, 
                            FILENAME = 'P:\Program Files\Microsoft SQL Server\MSSQL14.HAMANN\MSSQL\DATA\MyShop.mdf') 
                            LOG ON (NAME = MyShop_Log, 
                            FILENAME = 'P:\Program Files\Microsoft SQL Server\MSSQL14.HAMANN\MSSQL\DATA\MyShopLog.ldf')"; 

                var command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();
                Connection.Close();

                Connection = new SqlConnection(connectSt);
                Console.WriteLine("Database was created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR! Can't connect to the database or create the new one. {ex.Message}");
            }
        }

        private void CreateInstances()
        {
            Suppliers = new Suppliers(Connection);
            Categories = new Categories(Connection);
            Products = new Products(Connection);
        }

    }
}
