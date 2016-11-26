using System;
using Xunit;
using BangazonFinancialReports;
using Microsoft.Data.Sqlite;

namespace BangazonFinancialReportsTests
{
    public class DatabaseTest
    {
        bool truthy = true;
        DatabaseConnection DatabaseConnection = new DatabaseConnection();
        SqliteCommand sqliteCommand = new SqliteCommand();

        [Fact]
        public void CanOpenConnectionToTheDatabase()
        {

            var connectionString = DatabaseConnection.ConnectionString();
            Assert.NotNull(DatabaseConnection.ConnectionString());
            Assert.Equal(connectionString, DatabaseConnection.ConnectionString());
            Assert.Equal(connectionString, $"Filename={System.Environment.GetEnvironmentVariable("REPORTING_DB_PATH")}");

        }

        [Fact]

        public void CanTestDataBaseIsSeeded()
        {
            var connectionString = DatabaseConnection.ConnectionString();
            sqliteCommand.Connection = new SqliteConnection(connectionString);
            DatabaseConnection.execute(@"
            SELECT *
            FROM REVENUE
            WHERE Id = 1000", (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    truthy = false;
                    Assert.False(truthy);
                }
            
            });
        }
    }
}


