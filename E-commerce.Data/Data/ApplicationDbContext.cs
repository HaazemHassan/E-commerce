using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace E_commerce.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly static List<Category> _categories = new List<Category>();

        public virtual DbSet<Category> Categories { get; set; }

        static ApplicationDbContext()
        {
            string categoriesJson = File.ReadAllText("CategoriesSeedData.json");
            _categories = JsonSerializer.Deserialize<List<Category>>(categoriesJson)!;

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(_categories);

            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        }


    }
}