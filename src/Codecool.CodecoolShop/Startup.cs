using System;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codecool.CodecoolShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
            services.AddScoped<IProductCategoryDao, ProductCategoryDaoMemory>();
            services.AddScoped<IProductDao, ProductDaoMemory>();
            services.AddScoped<ISupplierDao, SupplierDaoMemory>();
            services.AddScoped<IShoppingCartDao, ShoppingCartDaoMemory>();
            services.AddScoped<IShoppingCartItemDao, ShoppingCartItemDaoMemory>();
            services.AddScoped<IOrderItemDao, OrderItemDaoMemory>();
            services.AddScoped<IClientDao, ClientItemDaoMemory>();


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
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });

        }

    }
}
