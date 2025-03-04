using E_commerce.UI.DTO;
using E_commerce.UI.Models;

namespace E_commerce.UI.ServicesContracts
{
    public interface ICategoriesService
    {
        public Task<List<CategoryResponse>> GetAllCategories();
        public Task<CategoryResponse> Create(CategoryAddRequest category);
        public Task<bool> IsCategoryNameExists(string name);

        public Task<CategoryResponse?> GetCategoryById(int? id);


        //should be CategoryUpdateRequest but ok :)
        public Task<CategoryResponse?> UpdateCategory(CategoryUpdateRequest category);

        public Task<CategoryResponse?> DeleteCategoryById(int? id);

    }
}
