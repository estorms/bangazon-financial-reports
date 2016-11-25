using System;
using Xunit;
using BangazonFinancialReports;

namespace BangazonFinancialReportsTests
{
    public class DatabaseTest
    {
        DatabaseSeed databaseSeed = new DatabaseSeed();
        [Fact]
        public void CanOpenConnectionToTheDatabase()
        {
        
        var connectionString = databaseSeed.ConnectionString();
        Assert.NotNull(databaseSeed.ConnectionString());
        Assert.Equal(connectionString, databaseSeed.ConnectionString());
        Assert.Equal(connectionString, $"Filename={System.Environment.GetEnvironmentVariable("REPORTING_DB_PATH")}");
 
        }

        

    }
}


