using E_commerce.Helpers;
using E_commerce.Models;
using E_commerce.Models.DTO;
using E_commerce.Models.Extentions;
using E_commerce.UI.ServicesContracts;
using RepositoriesContracts;
using System.Linq.Expressions;

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
            return (await _categoriesRepository.GetAll()).Select(Category => Category.ToCategoryResponse()).ToList();
        }

        public async Task<CategoryResponse> Create(CategoryAddRequest? category)
        {
            ValidationHelper.Validate(category);

            return (await _categoriesRepository.Create(category!.ToCategory())).ToCategoryResponse();
        }


        public async Task<CategoryResponse?> GetCategoryByName(string name)
        {
            return (await _categoriesRepository.Get(c => c.Name == name))?.ToCategoryResponse();

        }



        public async Task<CategoryResponse?> GetCategoryById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return (await _categoriesRepository.Get(c => c.Id == id.GetValueOrDefault()))?.ToCategoryResponse();
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
            if (id == null)
                return null;
            Category? category = await _categoriesRepository.Get(c => c.Id == id);
            if (category is null)
            {
                return null;
            }
            var response = await _categoriesRepository.Delete(category);
            if (response is null)
                return null;
            return response.ToCategoryResponse();
        }
    }
}
