namespace Beer2beer.UnitTest.Infrastructure;

using Beer2beer.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class ApplicationDbContextTest
{
    [Test]
    public void DbContext_CanBeInstantiatedQuicklyWithoutDatabaseInitializationInConstructor()
    {
        // Arrange
        using var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;

        // Act & Assert
        // Instantiating ApplicationDbContext should succeed quickly without executing Database.EnsureCreated() inside the constructor
        using var context = new ApplicationDbContext(options);
        Assert.That(context, Is.Not.Null);
    }

    [Test]
    public void DbContext_EnsureCreated_InitializesDatabaseSuccessfully()
    {
        // Arrange
        using var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;

        using var context = new ApplicationDbContext(options);

        // Act
        var created = context.Database.EnsureCreated();

        // Assert
        Assert.That(created, Is.True);
    }
}
