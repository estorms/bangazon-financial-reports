using System;
using System.Collections.Generic;
using Xunit;
using BangazonFinancialReports;
using Microsoft.Data.Sqlite;

namespace BangazonFinancialReportsTests
{

    public class RevenueFactoryTests
    {

        RevenueFactory revenueFactory = new RevenueFactory();

        [Fact]
        public void RevenueFactoryHasAMethodToGetAllRevenueEntries()
        {
            var AllRevenue = revenueFactory.GetAllRevenueEntries();
            Assert.Equal(typeof(List<Revenue>), AllRevenue.GetType());
            Assert.NotNull(revenueFactory.GetAllRevenueEntries());


        }

        [Fact]
        public void RevenueFactoryHasAMethodToGetRevenueByProduct()
        {
            Assert.NotNull(revenueFactory.GetRevenueByProduct());
            var RevenueByProduct= revenueFactory.GetRevenueByProduct();
            Assert.Equal(typeof(Dictionary<string, int>), RevenueByProduct.GetType());

        }

        [Fact]
        public void RevenueFactoryHasAMethodToGetRevenueByCustomer()
        {
            Assert.NotNull(revenueFactory.GetRevenueByCustomer());
            var RevenueByCustomer = revenueFactory.GetRevenueByCustomer();
            Assert.Equal(typeof(Dictionary<string, int>), RevenueByCustomer.GetType());

        }

    



        





    }
}