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
    public void ApplicationDbContext_ConfiguresIndexOnCustomerEmail()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        // Act
        using var context = new ApplicationDbContext(options);
        var entityType = context.Model.FindEntityType(typeof(Customer));

        // Assert
        Assert.That(entityType, Is.Not.Null);
        var emailIndex = entityType.GetIndexes()
            .FirstOrDefault(i => i.Properties.Any(p => p.Name == nameof(Customer.Email)));

        Assert.That(emailIndex, Is.Not.Null, "An index on Customer.Email should be configured.");
        Assert.That(emailIndex!.IsUnique, Is.True, "The index on Customer.Email should be unique.");
    }
}
