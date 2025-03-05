using E_commerce.DataAccess.Data;
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using RepositoriesContracts;
using System.Linq.Expressions;

namespace Repositories
{
    public class CategoriesRepository : Repository<Category>, ICategoriesRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            Category? oldCategory = await _db.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (oldCategory == null)
                return null;

            bool isNameTaken = await _db.Categories.AnyAsync(c => c.Name == category.Name && c.Id != category.Id);
            if (isNameTaken)
                throw new ArgumentException("Category name is already taken by another category");


            //better than _db.Update(category)
            //because this will update only the changed vaues
            _db.Entry(oldCategory).CurrentValues.SetValues(category);
            await _db.SaveChangesAsync();
            return category;

        }

    }
}
