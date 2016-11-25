using System;
using Xunit;
using BangazonFinancialReports;

namespace BangazonFinancialReportsTests
{
    public class DatabaseTest
    {
        DatabaseConnection DatabaseConnection = new DatabaseConnection();
        [Fact]
        public void CanOpenConnectionToTheDatabase()
        {
        
        var connectionString = DatabaseConnection.ConnectionString();
        Assert.NotNull(DatabaseConnection.ConnectionString());
        Assert.Equal(connectionString, DatabaseConnection.ConnectionString());
        Assert.Equal(connectionString, $"Filename={System.Environment.GetEnvironmentVariable("REPORTING_DB_PATH")}");
 
        }

    }
}


