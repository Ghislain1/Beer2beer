namespace Beer2beer.UnitTest.Infrastructure;

using Beer2beer.Core.Entities;
using Beer2beer.Infrastructure.Data;
using Beer2beer.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class CustomerRepositoryTest
{
    private SqliteConnection _connection = null!;
    private ApplicationDbContext _dbContext = null!;
    private CustomerRepository _repository = null!;

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
        var customer = new Customer { FullName = "John Doe", Email = "john@example.com" };
        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync();

        var exists = await _repository.IsExists("Email", "john@example.com");
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task IsExists_ReturnsFalse_WhenRecordDoesNotExist()
    {
        var exists = await _repository.IsExists("Email", "nonexistent@example.com");
        Assert.That(exists, Is.False);
    }

    [Test]
    public async Task IsExistsForUpdate_ReturnsTrue_WhenAnotherRecordHasSameKey()
    {
        var customer1 = new Customer { FullName = "John Doe", Email = "john@example.com" };
        var customer2 = new Customer { FullName = "Jane Doe", Email = "jane@example.com" };
        _dbContext.Customers.AddRange(customer1, customer2);
        await _dbContext.SaveChangesAsync();

        // Customer 2 trying to update email to customer 1's email
        var exists = await _repository.IsExistsForUpdate(customer2.Id, "Email", "john@example.com");
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task IsExistsForUpdate_ReturnsFalse_WhenSameRecordHasSameKey()
    {
        var customer1 = new Customer { FullName = "John Doe", Email = "john@example.com" };
        _dbContext.Customers.Add(customer1);
        await _dbContext.SaveChangesAsync();

        // Customer 1 updating their own profile keeping same email
        var exists = await _repository.IsExistsForUpdate(customer1.Id, "Email", "john@example.com");
        Assert.That(exists, Is.False);
    }
}
