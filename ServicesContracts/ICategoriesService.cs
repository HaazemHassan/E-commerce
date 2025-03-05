using E_commerce.Models;
using E_commerce.Models.DTO;
using System.Linq.Expressions;

namespace E_commerce.UI.ServicesContracts
{
    public interface ICategoriesService
    {
        public Task<List<CategoryResponse>> GetAllCategories();
        public Task<CategoryResponse> Create(CategoryAddRequest category);

        public Task<CategoryResponse?> GetCategoryById(int? id);


        //should be CategoryUpdateRequest but ok :)
        public Task<CategoryResponse?> UpdateCategory(CategoryUpdateRequest category);

        public Task<CategoryResponse?> DeleteCategoryById(int? id);
        public Task<CategoryResponse?> GetCategoryByName(string name);


    }
}
