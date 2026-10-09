

namespace Beer2beer.Infrastructure.Data;
using Beer2beer.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // Performance optimization: Avoid calling Database.EnsureCreated() in constructor.
        // Executing EnsureCreated() here triggers database schema inspection queries on every scoped
        // DbContext instantiation (per request). Database initialization is handled once at app startup in Program.cs.
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
