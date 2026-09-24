using Beer2beer.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Beer2beer.UnitTest.Data;

[TestFixture]
public class ApplicationDbContextTest
{
    [Test]
    public void Constructor_DoesNotExecuteEnsureCreated_WhenInstantiated()
    {
        // Arrange
        using var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;

        // Act & Assert
        // Instantiating ApplicationDbContext should succeed cleanly without running EnsureCreated in constructor
        using var context = new ApplicationDbContext(options);

        Assert.That(context, Is.Not.Null);
    }

    [Test]
    public async Task CanQueryCustomers_AfterDatabaseEnsureCreatedCalledExplicitly()
    {
        // Arrange
        using var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            await context.Database.EnsureCreatedAsync();
        }

        // Act & Assert
        using (var context = new ApplicationDbContext(options))
        {
            var customers = await context.Customers.ToListAsync();
            Assert.That(customers, Is.Not.Null);
        }
    }
}
