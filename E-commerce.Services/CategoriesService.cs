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
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesService(IUnitOfWork categoriesRepository)
        {
            _unitOfWork = categoriesRepository;
        }


        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            return ( await _unitOfWork.Categories.GetAll()).Select(Category => Category.ToCategoryResponse()).ToList();
        }

        public async Task<CategoryResponse> Create(CategoryAddRequest? category)
        {
            ValidationHelper.Validate(category);

            CategoryResponse categoryResponse = (await _unitOfWork.Categories.Create(category!.ToCategory())).ToCategoryResponse();
            await _unitOfWork.CompleteAsync();
            return categoryResponse;
        }


        public async Task<CategoryResponse?> GetCategoryByName(string name)
        {
            CategoryResponse? categoryResponse = (await _unitOfWork.Categories.Get(c => c.Name == name))?.ToCategoryResponse();
            return categoryResponse;

        }



        public async Task<CategoryResponse?> GetCategoryById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            CategoryResponse? categoryResponse = (await _unitOfWork.Categories.Get(c => c.Id == id.GetValueOrDefault()))?.ToCategoryResponse();
            return categoryResponse;

        }

        public async Task<CategoryResponse?> UpdateCategory(CategoryUpdateRequest? category)
        {
            ValidationHelper.Validate(category);

            var response = await _unitOfWork.Categories.Update(category!.ToCategory());
            if (response is null)
                return null;
            await _unitOfWork.CompleteAsync();

            return response.ToCategoryResponse();

        }

        public async Task<CategoryResponse?> DeleteCategoryById(int? id)
        {
            if (id == null)
                return null;
            Category? category = await _unitOfWork.Categories.Get(c => c.Id == id);
            if (category is null)
            {
                return null;
            }
            var response = _unitOfWork.Categories.Delete(category);
            if (response is null)
                return null;

            await _unitOfWork.CompleteAsync();
            return response.ToCategoryResponse();
        }
    }
}
