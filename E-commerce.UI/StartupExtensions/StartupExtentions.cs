using E_commerce.DataAccess.Data;
using E_commerce.UI.Services;
using E_commerce.UI.ServicesContracts;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoriesContracts;
using Microsoft.AspNetCore.Identity;
using E_commerce.Models.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.UI.StartupExtensions
{
    public static class StartupExtentions
    {

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllersWithViews(options =>
            //this is to enable antiforgery token validation for all post requests
            options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
            );

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<ICompaniesRepository, CompaniesRepository>();
            builder.Services.AddScoped<ICompaniesService, CompaniesService>();
            builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            builder.Services.AddScoped<IApplicationUsersService, ApplicationUsersService>();
            builder.Services.AddScoped<IShoppingCartsRepository, ShoppingCartRepository>();
            builder.Services.AddScoped<IShoppingCartsService, ShoppingCartsService>();


            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            //enable ahthorization (checks for auzorize attribute)
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                //options.Cookie.SameSite = SameSiteMode.Lax;  default
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            return builder;
        }
    }
}
