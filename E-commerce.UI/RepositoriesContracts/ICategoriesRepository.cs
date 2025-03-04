using E_commerce.UI.Models;
using System.Linq.Expressions;

namespace E_commerce.UI.RepositoriesContracts
{
    public interface ICategoriesRepository
    {
        public Task<List<Category>> GetAllCategories();
        //public Task<Category> GetCategoryById(int id);
        public Task<Category> Create(Category category);
        //public Task<Category> UpdateCategory(Category category);
        //Task<bool> ExistsAsync(Expression<Func<Category, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<Category, bool>> predicate);
        Task<Category?> GetCategoryById(int id);
        public Task<Category?> UpdateCategory(Category category);
        public Task<Category?> DeleteCategoryById(int id);

    }
}
