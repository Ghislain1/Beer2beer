namespace Beer2beer.UnitTest.Infrastructure;

using Beer2beer.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Diagnostics;

[TestFixture]
public class DbContextPerformanceTest
{
    private SqliteConnection _connection = null!;
    private DbContextOptions<ApplicationDbContext> _options = null!;

    [SetUp]
    public void SetUp()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        // Initialize database schema once at startup/setup
        using var initContext = new ApplicationDbContext(_options);
        initContext.Database.EnsureCreated();
    }

    [TearDown]
    public void TearDown()
    {
        _connection.Close();
        _connection.Dispose();
    }

    [Test]
    public void DbContext_Instantiation_ShouldBeFastWithoutPerRequestEnsureCreated()
    {
        // Measure time taken to instantiate DbContext 100 times without EnsureCreated in constructor
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < 100; i++)
        {
            using var context = new ApplicationDbContext(_options);
            Assert.That(context.Customers, Is.Not.Null);
        }
        stopwatch.Stop();

        // 100 instantiations should easily complete in under 500 milliseconds (typically < 10ms)
        Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(500), $"100 DbContext instantiations took {stopwatch.ElapsedMilliseconds} ms");
    }
}
