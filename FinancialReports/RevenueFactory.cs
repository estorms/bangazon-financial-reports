using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;

namespace BangazonFinancialReports
{
    public class RevenueFactory
    {
        public List<Revenue> GetAllRevenueEntries()
        {

        DatabaseConnection conn = new DatabaseConnection();
        List<Revenue> AllRevenue = new List<Revenue>();
        conn.execute(@"select * FROM Revenue",
                (SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        AllRevenue.Add(new Revenue
                        {
                            ProductName = reader[1].ToString(),
                            ProductCost = reader.GetInt32(2),
                            ProductRevenue = reader.GetInt32(3),
                            ProductSupplierState = reader[4].ToString(),
                            CustomerFirstName = reader[5].ToString(),
                            CustomerLastName = reader[6].ToString(),
                            CustomerAddress = reader[7].ToString(),
                            CustomerZipCode = reader.GetInt32(8),
                            PurchaseDate = reader.GetDateTime(9)
                        });
                    }
                });
            return AllRevenue;
        }
    }
}
