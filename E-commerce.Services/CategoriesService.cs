using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoriesService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            return ( await _unitOfWork.Categories.GetAll()).Select(Category => Category.ToCategoryResponse(_mapper)).ToList();
        }

        public async Task<CategoryResponse> Create(CategoryAddRequest? category)
        {
            ValidationHelper.Validate(category);

            CategoryResponse categoryResponse = (await _unitOfWork.Categories.Create(category!.ToCategory(_mapper))).ToCategoryResponse(_mapper);
            await _unitOfWork.CompleteAsync();
            return categoryResponse;
        }


        public async Task<CategoryResponse?> GetCategoryByName(string name)
        {
            CategoryResponse? categoryResponse = (await _unitOfWork.Categories.Get(c => c.Name == name))?.ToCategoryResponse(_mapper);
            return categoryResponse;

        }



        public async Task<CategoryResponse?> GetCategoryById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            CategoryResponse? categoryResponse = (await _unitOfWork.Categories.Get(c => c.Id == id.GetValueOrDefault()))?.ToCategoryResponse(_mapper);
            return categoryResponse;

        }

        public async Task<CategoryResponse?> UpdateCategory(CategoryUpdateRequest? category)
        {
            ValidationHelper.Validate(category);

            var response = await _unitOfWork.Categories.Update(category!.ToCategory(_mapper));
            if (response is null)
                return null;
            await _unitOfWork.CompleteAsync();

            return response.ToCategoryResponse(_mapper);

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
            return response.ToCategoryResponse(_mapper);
        }
    }
}
