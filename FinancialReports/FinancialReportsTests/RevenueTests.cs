using System;
using System.Collections.Generic;
using Xunit;


namespace BangazonFinancialReports
{

    public class RevenueTest
    {

        public static DateTime purchaseDate = DateTime.Now;
        Revenue revenue = new Revenue
        {
            Id = 3,
            ProductName = "JoshuaTree",
            ProductCost = 33,
            ProductRevenue = 45,
            ProductSupplierState = "NV",
            CustomerFirstName = "Jessica",
            CustomerLastName = "Brawner",
            CustomerAddress = "47777 Beautiful Lane",
            CustomerZipCode = 34721,
            PurchaseDate = purchaseDate
        };

        [Fact]
        public void trueTest()
        {
            bool truthy = true;
            Assert.Equal(truthy, true);

        }

        [Fact]

        public void RevenueClassHasId()
        {

            Assert.NotNull(revenue.Id);
            Assert.Equal(3, revenue.Id);
        }

        [Fact]

        public void RevenueClassHasProductName()
        {

            Assert.NotNull(revenue.ProductName);
            Assert.Equal("JoshuaTree", revenue.ProductName);
        }
        [Fact]

        public void RevenueClassHasProductCost()
        {

            Assert.NotNull(revenue.ProductCost);
            Assert.Equal(33, revenue.ProductCost);
        }

        [Fact]

        public void RevenueClassHasProductRevenue()
        {

            Assert.NotNull(revenue.ProductRevenue);
            Assert.Equal(45, revenue.ProductRevenue);
        }
        [Fact]

        public void RevenueClassHasProductSupplierState()
        {

            Assert.NotNull(revenue.ProductSupplierState);
            Assert.Equal("NV", revenue.ProductSupplierState);
        }
        [Fact]

        public void RevenueClassHasCustomerFirstName()
        {

            Assert.NotNull(revenue.CustomerFirstName);
            Assert.Equal("Jessica", revenue.CustomerFirstName);
        }
        [Fact]

        public void RevenueClassHasCustomerLastName()
        {

            Assert.NotNull(revenue.CustomerLastName);
            Assert.Equal("Brawner", revenue.CustomerLastName);
        }
        [Fact]

        public void RevenueClassHasCustomerAddress()
        {

            Assert.NotNull(revenue.CustomerAddress);
            Assert.Equal("47777 Beautiful Lane", revenue.CustomerAddress);
        }
        [Fact]

        public void RevenueClassHasCustomerZipCode()
        {

            Assert.NotNull(revenue.CustomerZipCode);
            Assert.Equal(34721, revenue.CustomerZipCode);
        }

        [Fact]
          public void RevenueClassHasPurchaseDate()
        {

            Assert.NotNull(revenue.PurchaseDate);
            Assert.Equal(purchaseDate, revenue.PurchaseDate);
        }

    }
}