using E_commerce.Models;
using System.Linq.Expressions;

namespace RepositoriesContracts
{
    public interface ICategoriesRepository : IRepository<Category> 
    {
      
        public Task<Category?> UpdateCategory(Category category);
   
    
    }
}
