

namespace Beer2beer.Infrastructure.Data;
using Beer2beer.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // Performance optimization (Bolt): Removed Database.EnsureCreated() from constructor.
        // ApplicationDbContext is scoped per HTTP request. Executing EnsureCreated() on every
        // context instantiation performs redundant schema queries on every request.
        // Database initialization is handled once at application startup.
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
