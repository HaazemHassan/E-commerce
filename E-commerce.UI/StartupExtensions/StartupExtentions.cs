using E_commerce.DataAccess.Data;
using E_commerce.UI.Services;
using E_commerce.UI.ServicesContracts;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoriesContracts;
using Microsoft.AspNetCore.Identity;
using E_commerce.Models.IdentityEntities;

namespace E_commerce.UI.StartupExtensions
{
    public static class StartupExtentions
    {

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
            builder.Services.AddScoped<IProductsService, ProductsService>();

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();
            return builder;
        }
    }
}
