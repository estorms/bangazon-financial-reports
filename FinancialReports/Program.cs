using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace BangazonFinancialReports
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime EndOfWeek = DateTime.Today.AddDays(-7);
            DateTime EndOfMonth = DateTime.Today.AddDays(-30);
            DateTime EndOfQuarter = DateTime.Today.AddDays(-120);
            bool dbIsFullySeeded = false;
            DatabaseConnection DatabaseConnection = new DatabaseConnection();
            string connectionstring = DatabaseConnection.ConnectionString();
            SqliteCommand sqliteCommand = new SqliteCommand();
            sqliteCommand.Connection = new SqliteConnection(connectionstring);

            try
            {
                DatabaseConnection.execute(@"
            SELECT Id
            FROM REVENUE
            WHERE Id = 1000", (SqliteDataReader reader) =>
                {

                    while (reader.Read())
                    {
                        dbIsFullySeeded = true;
                    }
                });
                // Console.WriteLine("database has already been seeded");
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                sqliteCommand.CommandType = CommandType.Text;
                DatabaseConnection.createTables();
                // Console.WriteLine("database has just been seeded");

            }

            RevenueFactory revenueFactory = new RevenueFactory();
            var AllRevenue = revenueFactory.GetAllRevenueEntries();
            var RevenueByProduct = revenueFactory.GetRevenueByProduct();
            List<Revenue> RevenueToPrint = new List<Revenue>();
            Console.WriteLine("Bangazon Reports");
            bool go_on = true;

            while (go_on)
            {
                try
                {

                    Console.WriteLine("1 - Last Week's Report");
                    Console.WriteLine("2 - Last Month's Report");
                    Console.WriteLine("3 - Last Quarter's Report");
                    Console.WriteLine("4 - Revenue by Customer");
                    Console.WriteLine("5 - Revenue by Product");

                    string userChoice = Console.ReadLine();
                    Console.WriteLine(userChoice);

                    switch (userChoice)
                    {
                        case "1":
                            foreach (var r in AllRevenue)
                            {
                                if (r.PurchaseDate > EndOfWeek)
                                {
                                    RevenueToPrint.Add(r);
                                }
                            }

                            foreach (var r in RevenueToPrint)
                            {
                                Console.WriteLine(string.Format("{0} was purchased with ${1}.00 in revenue.", r.ProductName, r.ProductCost));
                            }
                            break;

                        case "2":
                            {
                                foreach (var r in AllRevenue)
                            {
                                if (r.PurchaseDate > EndOfMonth)
                                {
                                    RevenueToPrint.Add(r);
                                }
                            }

                            foreach (var r in RevenueToPrint)
                            {
                                Console.WriteLine(string.Format("{0} was purchased with ${1}.00 in revenue.", r.ProductName, r.ProductCost));
                            }
                                break;
                            }

                        case "3":
                            {
                        foreach (var r in AllRevenue)
                            {
                                if (r.PurchaseDate > EndOfQuarter)
                                {
                                    RevenueToPrint.Add(r);
                                }
                            }

                            foreach (var r in RevenueToPrint)
                            {
                                Console.WriteLine(string.Format("{0} was purchased with ${1}.00 in revenue.", r.ProductName, r.ProductCost));
                            }
                                break;
                            }
                        case "4":
                            {
                                Console.WriteLine("case 4");
                                break;
                                
                            }

//NOT ALWAYS ORDERING CORRECTLY
                        case "5":
                            {
                                Console.WriteLine("case 5");
                                  foreach (KeyValuePair<string, int> r in RevenueByProduct)
                            {
                                Console.WriteLine(string.Format("Product Name: {0} Product Revenue: ${1}.00 in revenue.",  r.Key, r.Value));
                            }
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sorry an error has occcured. Please try agin ");
                    Console.WriteLine($"{ex}");
                    go_on = false;
                    Console.ReadKey();
                }
            }
        }
    }
}


