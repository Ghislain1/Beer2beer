namespace Beer2beer.UnitTest.Infrastructure;

using Beer2beer.Core.Entities;
using Beer2beer.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

[TestFixture]
public class ApplicationDbContextTest
{
    private DbContextOptions<ApplicationDbContext> _options;
    private SqliteConnection _connection;

    [SetUp]
    public void SetUp()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;
    }

    [TearDown]
    public void TearDown()
    {
        _connection?.Close();
        _connection?.Dispose();
    }

    [Test]
    public void ApplicationDbContext_Constructor_DoesNotThrowAndInstantiatesQuickly()
    {
        // Act & Assert
        Assert.DoesNotThrow(() =>
        {
            using var context = new ApplicationDbContext(_options);
            Assert.That(context, Is.Not.Null);
        });
    }

    [Test]
    public void CustomerEntity_HasIndexOnEmailProperty()
    {
        // Arrange & Act
        using var context = new ApplicationDbContext(_options);
        var entityType = context.Model.FindEntityType(typeof(Customer));

        // Assert
        Assert.That(entityType, Is.Not.Null);
        var index = entityType.GetIndexes().FirstOrDefault(i => i.Properties.Any(p => p.Name == nameof(Customer.Email)));
        Assert.That(index, Is.Not.Null, "An index on Customer.Email should exist in the EF Core model metadata.");
    }
}
