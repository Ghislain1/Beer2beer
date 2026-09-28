namespace Beer2beer.UnitTest.Infrastructure;

using Beer2beer.Core.Entities;
using Beer2beer.Infrastructure.Data;
using Beer2beer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class BaseRepositoryTest
{
    private ApplicationDbContext _context;
    private BaseRepository<Customer> _repository;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite($"Data Source={Guid.NewGuid()}.db")
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        _repository = new BaseRepository<Customer>(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task IsExists_ReturnsTrue_WhenRecordExists()
    {
        // Arrange
        var customer = new Customer
        {
            Id = 1001,
            FullName = "John Doe",
            Email = "john.unique@example.com",
            Balance = 100
        };
        await _repository.Create(customer);

        // Act
        var exists = await _repository.IsExists("Email", "john.unique@example.com");
        var notExists = await _repository.IsExists("Email", "nonexistent@example.com");

        // Assert
        Assert.That(exists, Is.True);
        Assert.That(notExists, Is.False);
    }

    [Test]
    public async Task IsExistsForUpdate_ReturnsTrue_WhenAnotherRecordHasSameKey()
    {
        // Arrange
        var customer1 = new Customer { Id = 1002, FullName = "John", Email = "john.update@example.com" };
        var customer2 = new Customer { Id = 1003, FullName = "Jane", Email = "jane.update@example.com" };
        await _repository.Create(customer1);
        await _repository.Create(customer2);

        // Act
        // Check if email "jane.update@example.com" exists for update of customer1 (should return true because customer2 has it)
        var exists = await _repository.IsExistsForUpdate(1002, "Email", "jane.update@example.com");
        // Check if email "john.update@example.com" exists for update of customer1 (should return false because it's customer1's own email)
        var sameRecord = await _repository.IsExistsForUpdate(1002, "Email", "john.update@example.com");

        // Assert
        Assert.That(exists, Is.True);
        Assert.That(sameRecord, Is.False);
    }
}
