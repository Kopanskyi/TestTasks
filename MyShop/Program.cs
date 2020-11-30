using MyShop.Model;
using System;
using System.Data;

namespace MyShop
{
    class Program
    {
        static void Main(string[] args)
        {
            // Before run this app, you should change connection string in DataManager class
            var manager = DataManager.Manager;
            Console.WriteLine();


            var query1 = @"Select * from Products where ProductName like 'C%'";
            var table1 = manager.Products.SelectFromTable(query1);
            Console.WriteLine($"Query: {query1} \nResult:");
            Show(table1);


            var query2 = @"Select Top(1) * from Products order by Price";
            var table2 = manager.Products.SelectFromTable(query2);
            Console.WriteLine($"Query: {query2} \nResult:");
            Show(table2);


            var query3 = @"Select Sum(ps.Price) as SummaryCost, ps.Country from
	                            (
		                            Select s.Country, p.Price from Products p
		                            inner join Suppliers s on p.SupplierID = s.SupplierID
	                            ) ps
                            group by ps.Country
                            having ps.Country = 'USA'";

            var table3 = manager.Products.SelectFromTable(query3);
            Console.WriteLine($"Query: {query3} \nResult:");
            Show(table3);


            var query4 = @"Select psc.CategoryName, psc.SupplierName, psc.City, psc.Country from
	                            (
		                            Select c.CategoryName, s.SupplierName, s.City, s.Country from Products p
		                            inner join Suppliers s on p.SupplierID = s.SupplierID
		                            inner join Categories c on p.CategoryID = c.CategoryID
	                            ) psc
                            where psc.CategoryName = 'Condiments'";

            var table4 = manager.Suppliers.SelectFromTable(query4);
            Console.WriteLine($"Query: {query4} \nResult:");
            Show(table4);


            manager.Suppliers.InsertIntoTable("Norske Meierier", "Lviv", "Ukraine");
            Console.WriteLine();
            manager.Products.InsertIntoTable("Green tea", "Norske Meierier", "Beverages", 10);


            Console.ReadKey();
        }

        public static void Show(DataTable table)
        {
            for (var i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    Console.Write($"{table.Rows[i].Field<object>(j)} | ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("\n");
        }
    }
}
