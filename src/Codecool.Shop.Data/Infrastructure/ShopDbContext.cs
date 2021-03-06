using Codecool.Shop.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Codecool.Shop.Data.Infrastructure;

public class ShopDbContext: IdentityDbContext<IdentityUser>
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options): base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Client> Clients { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        Supplier lenovo = new()
        {
            Id = 1,
            Name = "Amazon",
            Description = "Digital content and services"
        };
        Supplier amazon = new()
        {
            Id = 2,
            Name = "Lenovo",
            Description = "Computers"
        };
        modelBuilder.Entity<Supplier>().HasData(
            lenovo, amazon
        );

        ProductCategory tablet = new()
        {
            Id = 1,
            Name = "tablet",
            Department = "hardware",
            Description =
                "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display."
        };

        ProductCategory smartphone = new()
        {
            Id = 2,
            Name = "smartphone",
            Department = "hardware",
            Description =
                "A smartphone is a portable device that combines mobile telephone and computing functions into one unit. "
        };

        modelBuilder.Entity<ProductCategory>().HasData(
            tablet, smartphone
        );


        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Amazon Fire",
                DefaultPrice = 49.9,
                Currency = "USD",
                Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.",
                ProductCategoryId = tablet.Id,
                SupplierId = amazon.Id
            },
            new Product
            {
                Id = 2,
                Name = "Lenovo IdeaPad Miix 700",
                DefaultPrice = 479.0,
                Currency = "USD",
                Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.",
                ProductCategoryId = tablet.Id,
                SupplierId = lenovo.Id,
            },
            new Product
            {
                Id = 3,
                Name = "Amazon Fire HD 8",
                DefaultPrice = 89.0,
                Currency = "USD",
                Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.",
                ProductCategoryId = tablet.Id,
                SupplierId = amazon.Id,
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}