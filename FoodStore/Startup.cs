using FoodStore.Data;
using FoodStore.Infrastructure;
using FoodStore.Models.EFModels;
using FoodStore.Models.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<FoodStoreContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("FoodStoreConnection")));
            services.AddDbContext<IdentityDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityFoodStoreConnection"), options =>
                options.MigrationsAssembly("FoodStore"));
            });
            services.AddIdentity<IdentityUser, IdentityRole>(options => {

                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = true;

            }).AddEntityFrameworkStores<IdentityDbContext>();
            
            services.AddScoped<IProductRepo, EFProductModel>();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddSingleton<Trie>();
            services.AddSingleton<ProductDict>();
            services.AddAntiforgery(config => config.HeaderName = "XSRF-TOKEN");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("CategoryPage", "{category}Page{pageIndx:int}", 
                    new { Controller = "Home", Action = "HomeView" });

                endpoints.MapControllerRoute("Page", "Page{pageIndx:int}",
                   new { Controller = "Home", Action = "HomeView" });

                endpoints.MapControllerRoute("Category", "{category}",
                   new { Controller = "Home", Action = "HomeView" });

                endpoints.MapControllerRoute("Pagination", "Products/Page{pageIndx:int}", 
                    new { Controller = "Home", Action = "HomeView" });

                endpoints.MapControllerRoute("HomePage", "/", 
                    new { Controller = "Home", Action = "HomeView"});

                endpoints.MapRazorPages();
            });
        }
    }
}
