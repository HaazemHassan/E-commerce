using E_commerce.UI.DTO;
using E_commerce.UI.Helpers;
using E_commerce.UI.Models;
using E_commerce.UI.RepositoriesContracts;
using E_commerce.UI.ServicesContracts;

namespace E_commerce.UI.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }


        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            return (await _categoriesRepository.GetAllCategories()).Select(Category => Category.ToCategoryResponse()).ToList();
        }

        public async Task<CategoryResponse> Create(CategoryAddRequest? category)
        {
            ValidationHelper.Validate(category);

            return (await _categoriesRepository.Create(category!.ToCategory())).ToCategoryResponse();
        }


        public async Task<bool> IsCategoryNameExists(string name)
        {
            //is there any category with this name already exists ?
            return await _categoriesRepository.ExistsAsync(category => category.Name == name);
        }


        public async Task<CategoryResponse?> GetCategoryById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return (await _categoriesRepository.GetCategoryById(id.GetValueOrDefault()))?.ToCategoryResponse();
        }

        public async Task<CategoryResponse?> UpdateCategory(CategoryUpdateRequest? category)
        {
            ValidationHelper.Validate(category);

            var response = await _categoriesRepository.UpdateCategory(category!.ToCategory());
            if (response is null)
                return null;

            return response.ToCategoryResponse();

        }

        public async Task<CategoryResponse?> DeleteCategoryById(int? id)
        {

            var response = await _categoriesRepository.DeleteCategoryById(id.GetValueOrDefault());
            if (response is null)
                return null;
            return response.ToCategoryResponse();
        }
    }
}
