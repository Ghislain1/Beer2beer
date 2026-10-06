namespace Beer2beer.UnitTest.Infrastructure;

using Beer2beer.Core.Entities;
using Beer2beer.Infrastructure.Data;
using Beer2beer.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class BaseRepositoryTest
{
    private SqliteConnection _connection;
    private ApplicationDbContext _dbContext;
    private CustomerRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _repository = new CustomerRepository(_dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
        _connection.Dispose();
    }

    [Test]
    public async Task IsExists_ReturnsTrue_WhenRecordExists()
    {
        // Arrange
        var customer = new Customer
        {
            FullName = "Test Customer",
            Email = "test@example.com"
        };
        await _repository.Create(customer);

        // Act
        var exists = await _repository.IsExists("Email", "test@example.com");

        // Assert
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task IsExists_ReturnsFalse_WhenRecordDoesNotExist()
    {
        // Act
        var exists = await _repository.IsExists("Email", "nonexistent@example.com");

        // Assert
        Assert.That(exists, Is.False);
    }

    [Test]
    public async Task IsExistsForUpdate_ReturnsTrue_WhenAnotherRecordHasSameKey()
    {
        // Arrange
        var customer1 = new Customer { FullName = "Customer One", Email = "duplicate@example.com" };
        var customer2 = new Customer { FullName = "Customer Two", Email = "other@example.com" };
        await _repository.Create(customer1);
        await _repository.Create(customer2);

        // Act - check if customer2 trying to change to "duplicate@example.com" finds an existing record (customer1)
        var exists = await _repository.IsExistsForUpdate(customer2.Id, "Email", "duplicate@example.com");

        // Assert
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task IsExistsForUpdate_ReturnsFalse_WhenOnlyCurrentRecordHasKey()
    {
        // Arrange
        var customer1 = new Customer { FullName = "Customer One", Email = "mine@example.com" };
        await _repository.Create(customer1);

        // Act - customer1 checking if its own email exists for update
        var exists = await _repository.IsExistsForUpdate(customer1.Id, "Email", "mine@example.com");

        // Assert
        Assert.That(exists, Is.False);
    }
}
