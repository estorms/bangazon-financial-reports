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
            bool dbIsFullySeeded = false;
            DatabaseConnection DatabaseConnection = new DatabaseConnection();
            string connectionstring = DatabaseConnection.ConnectionString();
            SqliteCommand sqliteCommand = new SqliteCommand();
            sqliteCommand.Connection = new SqliteConnection(connectionstring);

            try
            {
                // SqliteDataReader reader;
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
                Console.WriteLine("database has already been seeded");
            }
            catch
            {

                sqliteCommand.CommandType = CommandType.Text;
                DatabaseConnection.createTables();
                Console.WriteLine("database necessarily seeded");

            }


            List<string> Names = new List<string>();
            List<string> Values = new List<string>();
            //Has to be a list of KeyValuePairs, rather than a dictionary, b/c a dictionary won't allow duplicate keys and product list has many
            List<KeyValuePair<string, int>> reportValues = new List<KeyValuePair<string, int>>();

            Console.WriteLine("Bangazon Reports");
            bool go_on = true;

            while (go_on)
            {
                try
                {

            Console.WriteLine("1 - Last Week Report");
            Console.WriteLine("2 - Last Month Report");
            Console.WriteLine("3 - Last 3 months Report");
            Console.WriteLine("4 - Rev by customer");
            Console.WriteLine("5 - Rev by product");

            var userChoice = Console.ReadLine();
            Console.WriteLine(userChoice);

            switch (userChoice)
            {
                case "1":
                    sqliteCommand.CommandText = "SELECT * FROM Revenue";
                    sqliteCommand.Connection.Open();
                   var reader = sqliteCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        //product name
                        var ProductName = reader[1];
                        //product name object to string (necessary b/c can't implicitly convert from obj to string)
                        var ProductNameToString = ProductName.ToString();
                        //product revenue
                        var productRevenue = reader[3];
                        //product revenue object to string
                        var ProductRevenueToString = productRevenue.ToString();
                        //product revenue string to int
                        var ProductRevenueInt  = int.Parse(ProductRevenueToString);
                        //purchase date
                        var PurchaseDate = reader[9];
                        //  purchase date to string
                        var PurchaseDateToString = PurchaseDate.ToString();
                        //parsing date from purchase-date string?
                        var ParsedPurchaseDate = DateTime.Parse(PurchaseDateToString);
                       //establishing end of prior week
                        var EndOfWeek = DateTime.Today.AddDays(-7);
                        //sets report data as key value pair, first value product name, second value product revenue
                        var reportData = new KeyValuePair<string, int>(ProductNameToString, ProductRevenueInt);

                        //conditional excludes products purchased during the current week from the report
                        if (ParsedPurchaseDate > EndOfWeek)
                        {
                            // proDict.Add(hh, e);     
                            reportValues.Add(reportData);
                        }
                    }


                    foreach (KeyValuePair<string, int> entry in reportValues)
                    {
                        Console.WriteLine(string.Format("{0} was purchased with ${1}.00 in revenue.", entry.Key, entry.Value));
                    }
                    break;
            }
                // case "2":
            //         sqliteCommand.CommandText = "SELECT * FROM Revenue";
            //         //
            //         sqliteCommand.Connection.Open();
            //         reader = sqliteCommand.ExecuteReader();
            //         while (reader.Read())
            //         {
            //             var hhh = reader[1];
            //             var o = hhh.ToString();
            //             var t = reader[3];


            //             var c = t.ToString();
            //             //  h
            //             var i = int.Parse(c);
            //             var k = reader[9];
            //             var e = k.ToString();
            //             var n = DateTime.Parse(e);
            //             var s = DateTime.Today.AddDays(-30);



            //             if (n > s)
            //             {
            //                 reportValues.Add(new KeyValuePair<string, int>(o, i));
            //             }
            //         }

            //         foreach (var y in reportValues)
            //         {
            //             var z = y.Key;
            //             var x = y.Value;
            //             var str = string.Format("{0} was purchased with ${1}.00 in revenue.", z, x);
            //             Console.WriteLine(str);
            //         }
            //         break;
            //     case "3":
            //         var h = DateTime.Today.AddDays(-90);

            //         sqliteCommand.CommandText = "SELECT * FROM Revenue WHERE PurchaseDate >= " + h;
            //         sqliteCommand.Connection.Open();
            //         reader = sqliteCommand.ExecuteReader();
            //         while (reader.Read())
            //         {
            //             var i = reader[1];
            //             var l = i.ToString();
            //             var o = reader[3];
            //             var v = o.ToString();
            //             var e = int.Parse(v);
            //             reportValues.Add(new KeyValuePair<string, int>(l, e));
            //         }

            //         foreach (var val in reportValues)
            //         {
            //             Console.WriteLine(string.Format("{0} was purchased with ${1}.00 in revenue.", val.Key, val.Value));
            //         }
            //         break;
            //     case "4":
            //         sqliteCommand.CommandText = string.Format("SELECT * FROM Revenue");
            //         sqliteCommand.Connection.Open();
            //         reader = sqliteCommand.ExecuteReader();
            //         //LIST DOESN'T WORK NEED DICTIONARY TO CHANGE VALUES
            //         Dictionary<string, int> customerReportValues = new Dictionary<string, int>();
            //         while (reader.Read())
            //         {
            //             if (customerReportValues.ContainsKey(reader[4].ToString()))
            //             {
            //                 customerReportValues[reader[4].ToString()] += int.Parse(reader[3].ToString());
            //             }
            //             else
            //             {
            //                 customerReportValues.Add(reader[4].ToString(), int.Parse(reader[3].ToString()));
            //             }
            //         }

            //         foreach (var val in customerReportValues)
            //         {
            //             Console.WriteLine(string.Format("{0} purchased items with a total of ${1}.00 in revenue.", val.Key, val.Value));
            //         }
            //         break;
            //     case "5":
            //         sqliteCommand.CommandText = string.Format("SELECT * FROM Revenue");
            //         sqliteCommand.Connection.Open();
            //         reader = sqliteCommand.ExecuteReader();

            //         //THERE HAS TO BE A BETTER WAY TO SORT
            //         Dictionary<string, int> productRevenue = new Dictionary<string, int>();
            //         SortedList<int, string> sortProductRevenue = new SortedList<int, string>();
            //         while (reader.Read())
            //         {
            //             if (productRevenue.ContainsKey(reader[1].ToString()))
            //             {
            //                 productRevenue[reader[1].ToString()] += int.Parse(reader[3].ToString());
            //             }
            //             else
            //             {
            //                 productRevenue.Add(reader[1].ToString(), int.Parse(reader[3].ToString()));
            //             }
            //         }
            //         foreach (var entry in productRevenue)
            //         {
            //             sortProductRevenue.Add(entry.Value, entry.Key);
            //         }
            //         foreach (var entry in sortProductRevenue)
            //         {
            //             Console.WriteLine(string.Format("Product: {0} Revenue: {1}", entry.Value, entry.Key));
            //         }

            //JUST IN CASE SORTING DOESN"T WORK
            // Dictionary<string, int> productsReportValues = new Dictionary<string, int>();
            // while (reader.Read())
            // {

            //     //Dictionary<string, int> productsReportValues = new Dictionary<string, int>();
            //     if (productsReportValues.ContainsKey(reader[1].ToString()))
            //     {
            //         productsReportValues[reader[1].ToString()] += int.Parse(reader[3].ToString());
            //     }
            //     else
            //     {
            //         productsReportValues.Add(reader[1].ToString(), int.Parse(reader[3].ToString()));
            //     }
            // }

            // foreach (var val in productsReportValues)
            //     {
            //         Console.WriteLine(string.Format("{0} brought in a total of ${1}.00 in revenue.", val.Key, val.Value));
            //     }
            //                 break;
            //             default:
            //                 Console.WriteLine("Invalid input. Try Again.");
            //                 break;
            //         }
            //         Console.ReadKey();
                }
                catch (Exception ex)
                {
                    //ADDING ERROR HANDLING
                    Console.WriteLine("Sorry an error has occcured. Please try agin ");
                    Console.WriteLine($"{ex}");
                    go_on = false;
                    Console.ReadKey();
                }
                }
            }
        }
}


