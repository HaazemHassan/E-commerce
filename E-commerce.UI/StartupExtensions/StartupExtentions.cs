using E_commerce.DataAccess.Data;
using E_commerce.UI.Services;
using E_commerce.UI.ServicesContracts;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoriesContracts;

namespace E_commerce.UI.StartupExtensions
{
    public static class StartupExtentions
    {

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            return builder;
        }
    }
}
