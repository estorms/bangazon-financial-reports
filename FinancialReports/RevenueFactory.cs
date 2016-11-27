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
        public DatabaseConnection dbConnection = new DatabaseConnection();
        public List<Revenue> GetAllRevenueEntries()
        {
            List<Revenue> AllRevenue = new List<Revenue>();
            dbConnection.execute(@"select * FROM Revenue",
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
        public Dictionary<string, int> GetRevenueByProduct()
        {

            Dictionary<string, int> RevenueByProduct = new Dictionary<string, int>();
            dbConnection.execute(@"Select Revenue.ProductName as 'Product Name',
            sum(Revenue.ProductRevenue) as 'Revenue per Product'
            from Revenue
            group by Revenue.ProductName
            order by 'Revenue per Product' desc",
                    (SqliteDataReader reader) =>
                    {
                        while (reader.Read())
                        {
                          RevenueByProduct.Add(reader[0].ToString(), reader.GetInt32(1));
                        }
                    });
            return RevenueByProduct;
        }
    }
}
