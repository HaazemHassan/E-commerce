using E_commerce.UI.Data;
using E_commerce.UI.Models;
using E_commerce.UI.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace E_commerce.UI.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Category> Create(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }


        public async Task<List<Category>> GetAllCategories()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _db.Categories.AnyAsync(predicate);
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            Category? oldCategory = await _db.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (oldCategory == null)
                return null;

            //better than _db.Update(category)
            //because this will update only the changed vaues
            _db.Entry(oldCategory).CurrentValues.SetValues(category);
            await _db.SaveChangesAsync();
            return category;

        }

        public async Task<Category?> DeleteCategoryById(int id)
        {
            var category = await GetCategoryById(id);
            if (category is null)
                return null;

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return category;
        }
    }
}
