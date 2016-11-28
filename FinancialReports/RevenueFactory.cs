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
            dbConnection.execute(@"select * FROM Revenue
            order by Revenue.ProductName",
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
            dbConnection.execute(@"Select Revenue.ProductName,
            sum(Revenue.ProductRevenue) 
            from Revenue
        
            group by Revenue.ProductName
            order by sum(Revenue.ProductRevenue) desc",  
    
                    (SqliteDataReader reader) =>
                    {
                        while (reader.Read())
                        {
                          RevenueByProduct.Add(reader[0].ToString(), reader.GetInt32(1));
                        }
                    });
            return RevenueByProduct;
        }

           public Dictionary<string, int> GetRevenueByCustomer()
        {

            Dictionary<string, int> RevenueByCustomer = new Dictionary<string, int>();
            dbConnection.execute(@"Select 
            Revenue.CustomerFirstName,
            Revenue.CustomerLastName,
            sum(Revenue.ProductRevenue) 
            from Revenue
            group by Revenue.CustomerFirstName
            order by sum(Revenue.ProductRevenue) desc",  
    
                    (SqliteDataReader reader) =>
                    {
                        while (reader.Read())
                        {
                        RevenueByCustomer.Add(reader[0].ToString() + " " + reader[1].ToString(), reader.GetInt32(2));
                        }
                    });
            return RevenueByCustomer;
        }
    }
}
