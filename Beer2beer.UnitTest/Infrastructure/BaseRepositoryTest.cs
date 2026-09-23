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
    private SqliteConnection _connection = null!;
    private ApplicationDbContext _dbContext = null!;
    private BaseRepository<Customer> _customerRepository = null!;

    [SetUp]
    public void SetUp()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _dbContext.Database.EnsureCreated();

        _customerRepository = new BaseRepository<Customer>(_dbContext);
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
        var customer = new Customer { FullName = "Test User", Email = "testuser@example.com" };
        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync();

        // Act
        var exists = await _customerRepository.IsExists("Email", "testuser@example.com");

        // Assert
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task IsExists_ReturnsFalse_WhenRecordDoesNotExist()
    {
        // Act
        var exists = await _customerRepository.IsExists("Email", "nonexistent@example.com");

        // Assert
        Assert.That(exists, Is.False);
    }

    [Test]
    public async Task IsExistsForUpdate_ReturnsTrue_WhenAnotherRecordHasSameEmail()
    {
        // Arrange
        var customer1 = new Customer { FullName = "User One", Email = "duplicate@example.com" };
        var customer2 = new Customer { FullName = "User Two", Email = "user2@example.com" };
        _dbContext.Customers.AddRange(customer1, customer2);
        await _dbContext.SaveChangesAsync();

        // Act - check if customer2 trying to change email to customer1's email returns true
        var exists = await _customerRepository.IsExistsForUpdate(customer2.Id, "Email", "duplicate@example.com");

        // Assert
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task IsExistsForUpdate_ReturnsFalse_WhenSameRecordHasSameEmail()
    {
        // Arrange
        var customer = new Customer { FullName = "User One", Email = "user1@example.com" };
        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync();

        // Act - check if customer updating without changing email returns false
        var exists = await _customerRepository.IsExistsForUpdate(customer.Id, "Email", "user1@example.com");

        // Assert
        Assert.That(exists, Is.False);
    }
}
