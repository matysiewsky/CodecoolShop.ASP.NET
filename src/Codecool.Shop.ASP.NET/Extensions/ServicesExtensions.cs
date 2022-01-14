using System;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.ASP.NET.Service.Services;
using Codecool.Shop.Data.Repositories;
using Codecool.Shop.Data.Repositories.Repositories;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codecool.Shop.ASP.NET.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureDbContextAndSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShopDbContext>(options =>
        {
            options.UseSqlServer(new SqlConnectionStringBuilder()
                {
                    DataSource = configuration["DbServer"],
                    InitialCatalog = configuration["DbName"],
                    UserID = configuration["DbUsername"],
                    Password = configuration["DbPassword"],
                    MultipleActiveResultSets = true
                }
                .ConnectionString);
            // options.EnableSensitiveDataLogging();
        });
    }

    public static void ConfigureSession(this IServiceCollection services)
    {
        services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            }
        );
    }

    public static void ConfigureDefaultIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<IdentityUser>()
            .AddEntityFrameworkStores<ShopDbContext>();
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGenericDbRepository<ProductCategory>, GenericDbRepository<ProductCategory>>(s => new GenericDbRepository<ProductCategory>()
        {
            DbContext = s.GetRequiredService<ShopDbContext>(),
            DbSet = s.GetRequiredService<ShopDbContext>().Set<ProductCategory>()
        });
        services.AddScoped<IGenericDbRepository<Product>, GenericDbRepository<Product>>(s => new GenericDbRepository<Product>()
        {
            DbContext = s.GetRequiredService<ShopDbContext>(),
            DbSet = s.GetRequiredService<ShopDbContext>().Set<Product>()
        });
        services.AddScoped<IGenericDbRepository<Supplier>, GenericDbRepository<Supplier>>(s => new GenericDbRepository<Supplier>()
        {
            DbContext = s.GetRequiredService<ShopDbContext>(),
            DbSet = s.GetRequiredService<ShopDbContext>().Set<Supplier>()
        });
        services.AddScoped<IGenericDbRepository<Cart>, GenericDbRepository<Cart>>(s => new GenericDbRepository<Cart>()
        {
            DbContext = s.GetRequiredService<ShopDbContext>(),
            DbSet = s.GetRequiredService<ShopDbContext>().Set<Cart>()
        });
        services.AddScoped<IGenericDbRepository<CartItem>, GenericDbRepository<CartItem>>(s => new GenericDbRepository<CartItem>()
        {
            DbContext = s.GetRequiredService<ShopDbContext>(),
            DbSet = s.GetRequiredService<ShopDbContext>().Set<CartItem>()
        });
        services.AddScoped<IGenericDbRepository<Order>, GenericDbRepository<Order>>(s => new GenericDbRepository<Order>()
        {
            DbContext = s.GetRequiredService<ShopDbContext>(),
            DbSet = s.GetRequiredService<ShopDbContext>().Set<Order>()
        });
        services.AddScoped<IGenericDbRepository<Client>, GenericDbRepository<Client>>(s => new GenericDbRepository<Client>()
        {
            DbContext = s.GetRequiredService<ShopDbContext>(),
            DbSet = s.GetRequiredService<ShopDbContext>().Set<Client>()
        });
    }

    public static void ConfigureAppServices(this IServiceCollection services)
    {
        services.AddScoped<ISupplierService, SupplierService>(s => new SupplierService
        {
            SupplierRepository = s.GetRequiredService<IGenericDbRepository<Supplier>>()
        });
        services.AddScoped<IProductService, ProductService>(s => new ProductService
        {
            ProductRepository = s.GetRequiredService<IGenericDbRepository<Product>>(),
            ProductCategoryRepository = s.GetRequiredService<IGenericDbRepository<ProductCategory>>(),
            SupplierService = s.GetRequiredService<ISupplierService>()
        });

        services.AddScoped<ICartService>(s => new CartService
        {
            ProductService = s.GetRequiredService<IProductService>(),
            CartRepository = s.GetRequiredService<IGenericDbRepository<Cart>>(),
            CartItemRepository = s.GetRequiredService<IGenericDbRepository<CartItem>>(),
            HttpContextAccessor = s.GetRequiredService<IHttpContextAccessor>()
        });
        services.AddScoped<IClientService, ClientService>(s => new ClientService
        {
            ClientRepository = s.GetRequiredService<IGenericDbRepository<Client>>(),
        });
        services.AddScoped<IOrderService, OrderService>(s => new OrderService
        {
            OrderRepository = s.GetRequiredService<IGenericDbRepository<Order>>(),
            CartService = s.GetRequiredService<ICartService>(),
            ProductService = s.GetRequiredService<IProductService>(),
        });
    }
}