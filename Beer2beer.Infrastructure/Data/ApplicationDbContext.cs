

namespace Beer2beer.Infrastructure.Data;
using Beer2beer.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : IdentityDbContext
{
    // Removed Database.EnsureCreated() from the constructor.
    // Calling EnsureCreated() on every DbContext instantiation (scoped per HTTP request)
    // executes redundant schema inspection queries against SQLite, causing significant request latency.
    // Database initialization is handled once during application startup.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    // public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    // public DbSet<Order> Orders { get; set; }
    //  public DbSet<OrderDetails> OrderDetails { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ApplicationDbContextConfigurations.Configure(builder);
        ApplicationDbContextConfigurations.SeedData(builder);


    }

}
