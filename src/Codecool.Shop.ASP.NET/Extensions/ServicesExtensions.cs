using System;
using Codecool.Shop.Data.Infrastructure;
using Codecool.Shop.Data.Infrastructure.Repository;
using Codecool.Shop.Domain.Repositories.Interfaces;
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
        services.AddDefaultIdentity<IdentityUser>
                (x => x.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<ShopDbContext>();
    }

    public static void ConfigureUnitOfWork(this IServiceCollection services)
        => services.AddTransient<IUnitOfWork, UnitOfWork>();


    // public static void ConfigureAppServices(this IServiceCollection services)
    // {
    //     services.AddScoped<ISupplierService, SupplierService>(s => new SupplierService
    //     {
    //         UnitOfWork = s.GetRequiredService<IUnitOfWork>()
    //     });
    //     services.AddScoped<IProductService, ProductService>(s => new ProductService
    //     {
    //         UnitOfWork = s.GetRequiredService<IUnitOfWork>()
    //     });
    //
    //     services.AddScoped<ICartService>(s => new CartService
    //     {
    //         UnitOfWork = s.GetRequiredService<IUnitOfWork>(),
    //         HttpContextAccessor = s.GetRequiredService<IHttpContextAccessor>()
    //     });
    //     services.AddScoped<IClientService, ClientService>(s => new ClientService
    //     {
    //         UnitOfWork = s.GetRequiredService<IUnitOfWork>()
    //     });
    //     services.AddScoped<IOrderService, OrderService>(s => new OrderService
    //     {
    //         UnitOfWork = s.GetRequiredService<IUnitOfWork>()
    //     });
    // }
}