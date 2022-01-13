using System;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.ASP.NET.Service.Services;
using Codecool.Shop.Data.Repositories;
using Codecool.Shop.Data.Repositories.Repositories;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codecool.Shop.ASP.NET;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(new SqlConnectionStringBuilder()
                {
                    DataSource = Configuration["DbServer"],
                    InitialCatalog = Configuration["DbName"],
                    UserID = Configuration["DbUsername"],
                    Password = Configuration["DbPassword"],
                    MultipleActiveResultSets = true
                }
                .ConnectionString);
            options.EnableSensitiveDataLogging();
        });
        services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);

            }
        );

        services.AddHttpContextAccessor();
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



        services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Product/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthorization();

        app.UseSession();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Products}/{id?}");
        });

    }

}