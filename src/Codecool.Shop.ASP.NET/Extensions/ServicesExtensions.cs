using System;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.ASP.NET.Service.Services;
using Codecool.Shop.Data.Repositories;
using Codecool.Shop.Data.Repositories.Repositories;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codecool.Shop.ASP.NET.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureDbContextAndSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
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

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductCategoryRepository, ProductCategoryDbRepository>(s => new ProductCategoryDbRepository()
        {
            AppDbContext = s.GetRequiredService<AppDbContext>()
        });
        services.AddScoped<IProductRepository, ProductDbRepository>(s => new ProductDbRepository()
        {
            AppDbContext = s.GetRequiredService<AppDbContext>()
        });
        services.AddScoped<ISupplierRepository, SupplierDbRepository>(s => new SupplierDbRepository()
        {
            AppDbContext = s.GetRequiredService<AppDbContext>()
        });
        services.AddScoped<ICartRepository, CartDbRepository>(s => new CartDbRepository()
        {
            AppDbContext = s.GetRequiredService<AppDbContext>()
        });
        services.AddScoped<ICartItemRepository, CartItemDbRepository>(s => new CartItemDbRepository()
        {
            AppDbContext = s.GetRequiredService<AppDbContext>()
        });
        services.AddScoped<IOrderRepository, OrderDbRepository>(s => new OrderDbRepository()
        {
            AppDbContext = s.GetRequiredService<AppDbContext>()
        });
        services.AddScoped<IClientRepository, ClientDbRepository>(s => new ClientDbRepository()
        {
            AppDbContext = s.GetRequiredService<AppDbContext>()
        });
    }

    public static void ConfigureAppServices(this IServiceCollection services)
    {
        services.AddScoped<ISupplierService, SupplierService>(s => new SupplierService
        {
            SupplierRepository = s.GetRequiredService<ISupplierRepository>()
        });
        services.AddScoped<IProductService, ProductService>(s => new ProductService
        {
            ProductRepository = s.GetRequiredService<IProductRepository>(),
            ProductCategoryRepository = s.GetRequiredService<IProductCategoryRepository>(),
            SupplierService = s.GetRequiredService<ISupplierService>()
        });

        services.AddScoped<ICartService>(s => new CartService
        {
            ProductService = s.GetRequiredService<IProductService>(),
            CartRepository = s.GetRequiredService<ICartRepository>(),
            CartItemRepository = s.GetRequiredService<ICartItemRepository>(),
            HttpContextAccessor = s.GetRequiredService<IHttpContextAccessor>()
        });
        services.AddScoped<IClientService, ClientService>(s => new ClientService
        {
            ClientRepository = s.GetRequiredService<IClientRepository>(),
        });
        services.AddScoped<IOrderService, OrderService>(s => new OrderService
        {
            OrderRepository = s.GetRequiredService<IOrderRepository>(),
            CartService = s.GetRequiredService<ICartService>(),
            ProductService = s.GetRequiredService<IProductService>(),
        });
    }
}