using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace BangazonProductRevenueReports
{
    public class DatabaseSeed
    {

        private static string _connectionString = $"Filename={System.Environment.GetEnvironmentVariable("REPORTING_DB_PATH")}";

        public string ConnectionString() {
            return _connectionString;
        }
        Random rnd = new Random();

        string[] customers = new[] { "Carys", "Emmett", "Latoya", "Trina", "Kade", "Torin", "Aggie", "Caelan", "Patsy", "Bettina", "Hans", "Leda", "Clair", "Evan", "Roscoe", "Sondra", "Dixon", "Gail" };
        string[] customersLastName = new[] { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin" };
        string[] products = new[] { "Rug", "Wine Glasses", "Book Ends", "Picture Frame", "Blu-Ray Player", "Digital Camera", "Stuffed Animal - Monkey", "Stuffed Animal - Sloth", "Spatula", "Crayons", "Headphones", "Lawn Furniture", "Hammer", "Computer Monitor", "Golf Clubs", "Raspberry Pi", "eReader" };
        int[] productprice = new[] { 57, 21, 16, 22, 95, 257, 4, 5, 10, 2, 53, 150, 25, 950, 860, 45, 250 };
        int[] productrevenue = new[] { 3, 1, 4, 2, 15, 52, 1, 1, 1, 1, 5, 53, 3, 24, 169, 10, 9 };
        string[] customerAddressNumbers = new[] { "123", "435", "44", "283a", "6b", "1440", "7723", "289", "7564", "985-b", "33922", "23", "546", "5692", "6780", "9362", "121", "74567", "18", "9" };
        string[] customerAddressStreet = new[] { "Mallory Lane", "Carothers Pkwy", "Claybrook Lane", "Bending Creek Drive", "Old Hickory Blvd", "Harris Ave", "21st Ave N", "Plus Park Blvd", "Interstate Blvd S", "Whitney Ave", "Bell Rd", "Harding Pky", "Nolesville Road", "Charlotte Ave" };
        int[] customerZipcode = new int[] { 37013, 37072, 38461, 37115, 37116, 37201, 37211, 37216, 37222 };
        string[] supplierState = new string[] { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HA", "ID", "IL", "IN", "IA", "KA", "KY", "LA", "ME", "MD", "MS", "MC", "MN", "MI", "MO", "MT", "NB", "NV", "NH", "NJ", "NC", "NY", "NM", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "WA", "WV", "WI", "WY" };

        public void createTables()
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(_connectionString);
                using (connection)
                {
                    connection.Open();

                    SqliteCommand command = connection.CreateCommand();
                    string RevenueTable = "CREATE TABLE Revenue (" +
                                    "[Id] INTEGER NOT NULL CONSTRAINT \"PK_Revenue\" PRIMARY KEY AUTOINCREMENT, " +
                                    "[ProductName] TEXT NOT NULL, " +
                                    "[ProductCost] INTEGER NOT NULL," +
                                    "[ProductRevenue] INTEGER NOT NULL, " +
                                    "[ProductSupplierState] TEXT NOT NULL, " +
                                    "[CustomerFirstName] TEXT NOT NULL, " +
                                    "[CustomerLastName] TEXT NOT NULL, " +
                                    "[CustomerAddress] TEXT NOT NULL, " +
                                    "[CustomerZipCode] INTEGER NOT NULL, " +
                                    "[PurchaseDate] TEXT NOT NULL DEFAULT (strftime('%Y-%m-%d %H:%M:%S')) " +
                                "); "
                                + RandomizeCustomerProducts(1000);
                    command.CommandText = RevenueTable;
                    command.ExecuteNonQuery();
                    // clean up
                    command.Dispose();
                    connection.Close();

                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex);
            }
        }
        public string RandomizeCustomerProducts()
        {
            var rnd1 = rnd.Next(customers.Length);
            var rnd2 = rnd.Next(customersLastName.Length);
            var rnd3 = rnd.Next(products.Length);
            var rnd4 = rnd.Next(customerAddressNumbers.Length);
            var rnd5 = rnd.Next(customerAddressStreet.Length);
            var rnd6 = rnd.Next(customerZipcode.Length);
            var rnd7 = rnd.Next(supplierState.Length);

            DateTime start = DateTime.Today.AddDays(-200);
            int range = (DateTime.Today - start).Days;

            string command = $@"
            INSERT INTO Revenue 
            VALUES (
                null,
                '{products[rnd3]}', 
                {productprice[rnd3]}, 
                {productrevenue[rnd3]}, 
                '{supplierState[rnd7]}', 
                '{customers[rnd1]}', 
                '{customersLastName[rnd2]}', 
                '{customerAddressNumbers[rnd4]} {customerAddressStreet[rnd5]}', 
                {customerZipcode[rnd6]}, 
                '{start.AddDays(rnd.Next(range))}'
            );";

            return command;
        }

        public string RandomizeCustomerProducts(int numOfEntries)
        {
            string returnstring = "";
            for (var i = 0; i < numOfEntries; i++) { returnstring += RandomizeCustomerProducts(); }
            return returnstring;
        }
    }
}