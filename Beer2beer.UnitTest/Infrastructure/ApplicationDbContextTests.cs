namespace Beer2beer.UnitTest.Infrastructure;

using Beer2beer.Core.Entities;
using Beer2beer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class ApplicationDbContextTests
{
    [Test]
    public void CustomerEntity_HasUniqueIndexOnEmail()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        // Act
        using var context = new ApplicationDbContext(options);
        var customerEntityType = context.Model.FindEntityType(typeof(Customer));

        // Assert
        Assert.That(customerEntityType, Is.Not.Null);

        var emailIndex = customerEntityType!.GetIndexes()
            .FirstOrDefault(idx => idx.Properties.Any(p => p.Name == nameof(Customer.Email)));

        Assert.That(emailIndex, Is.Not.Null, "Expected Customer entity to have an index on Email.");
        Assert.That(emailIndex!.IsUnique, Is.True, "Expected index on Customer.Email to be unique.");
    }
}
