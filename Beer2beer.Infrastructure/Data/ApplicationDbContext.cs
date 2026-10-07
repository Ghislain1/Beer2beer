

namespace Beer2beer.Infrastructure.Data;
using Beer2beer.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // Optimization: EnsureCreated() removed from constructor.
        // Calling EnsureCreated() here executed schema reflection and SQLite table checks on every scoped DbContext creation per HTTP request.
        // Database creation is now handled once during application startup in Program.cs.
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
