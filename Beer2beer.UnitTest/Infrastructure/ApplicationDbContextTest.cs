namespace Beer2beer.UnitTest.Infrastructure;

using Beer2beer.Core.Entities;
using Beer2beer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

[TestFixture]
public class ApplicationDbContextTest
{
    [Test]
    public void CustomerEntity_ShouldHaveUniqueIndexOnEmail()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        // Act
        using var dbContext = new ApplicationDbContext(options);
        var customerEntityType = dbContext.Model.FindEntityType(typeof(Customer));
        var emailProperty = customerEntityType?.FindProperty(nameof(Customer.Email));
        var emailIndex = emailProperty != null ? customerEntityType?.FindIndex(emailProperty) : null;

        // Assert
        Assert.That(customerEntityType, Is.Not.Null, "Customer entity type should be configured in ApplicationDbContext.");
        Assert.That(emailIndex, Is.Not.Null, "Customer entity should have an index on Email property.");
        Assert.That(emailIndex!.IsUnique, Is.True, "Customer Email index should be unique.");
    }
}
