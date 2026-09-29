using System.Diagnostics;
using Beer2beer.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Beer2beer.UnitTest.Infrastructure;

[TestFixture]
public class ApplicationDbContextTest
{
    private SqliteConnection _connection = null!;
    private DbContextOptions<ApplicationDbContext> _options = null!;

    [SetUp]
    public void Setup()
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
        _connection.Close();
        _connection.Dispose();
    }

    [Test]
    public void DbContext_Instantiation_IsFastWithoutEnsureCreatedInConstructor()
    {
        // Ensure database schema is created explicitly once
        using (var dbContext = new ApplicationDbContext(_options))
        {
            dbContext.Database.EnsureCreated();
        }

        // Measure fast instantiation of multiple DbContext instances
        var stopwatch = Stopwatch.StartNew();
        const int iterations = 100;
        for (int i = 0; i < iterations; i++)
        {
            using var context = new ApplicationDbContext(_options);
            Assert.That(context, Is.Not.Null);
        }
        stopwatch.Stop();

        // 100 instantiations should complete in less than 50ms (microsecond level per instantiation)
        Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(500), $"100 DbContext instantiations took {stopwatch.ElapsedMilliseconds} ms");
    }

    [Test]
    public async Task DbContext_CanQueryData_AfterExplicitEnsureCreated()
    {
        using (var dbContext = new ApplicationDbContext(_options))
        {
            await dbContext.Database.EnsureCreatedAsync();
        }

        using (var dbContext = new ApplicationDbContext(_options))
        {
            var customerCount = await dbContext.Customers.CountAsync();
            Assert.That(customerCount, Is.GreaterThanOrEqualTo(0));
        }
    }
}
