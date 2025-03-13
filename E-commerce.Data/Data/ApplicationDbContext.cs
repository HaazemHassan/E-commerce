using E_commerce.Models;
using E_commerce.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace E_commerce.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        private readonly static List<Category> _categories = new List<Category>();
        private readonly static List<Product> _products = new List<Product>();
        private readonly static List<Company> _companies = new List<Company>();

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

        static ApplicationDbContext()
        {
            string categoriesJson = File.ReadAllText("CategoriesSeedData.json");
            _categories = JsonSerializer.Deserialize<List<Category>>(categoriesJson)!;

            string productsJson = File.ReadAllText("ProductsSeedData.json");
            _products = JsonSerializer.Deserialize<List<Product>>(productsJson)!;

            string companiesJson = File.ReadAllText("CompaniesSeedData.json");
            _companies= JsonSerializer.Deserialize<List<Company>>(companiesJson)!;


        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(_categories);
            modelBuilder.Entity<Product>().HasData(_products);
            modelBuilder.Entity<Company>().HasData(_companies);

            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(p => p.ISBN).IsUnique();
        }


    }
}