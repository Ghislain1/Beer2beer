namespace Beer2beer.UnitTest.Infrastructure;

using Beer2beer.Core.Entities;
using Beer2beer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

[TestFixture]
public class ApplicationDbContextTest
{
    [Test]
    public void Customer_Email_ShouldHaveUniqueIndex()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        // Act
        using var dbContext = new ApplicationDbContext(options);
        var customerEntityType = dbContext.Model.FindEntityType(typeof(Customer));

        // Assert
        Assert.That(customerEntityType, Is.Not.Null);

        var emailProperty = customerEntityType.FindProperty(nameof(Customer.Email));
        Assert.That(emailProperty, Is.Not.Null);

        var indexes = customerEntityType.GetIndexes().ToList();
        var emailIndex = indexes.FirstOrDefault(i =>
            i.Properties.Count == 1 && i.Properties.Any(p => p.Name == nameof(Customer.Email)));

        Assert.That(emailIndex, Is.Not.Null, "Expected an index on Customer.Email for fast existence lookup");
        Assert.That(emailIndex.IsUnique, Is.True, "Expected index on Customer.Email to be unique");
    }
}
