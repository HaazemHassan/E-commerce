using AutoMapper;
using E_commerce.Models;
using E_commerce.Models.DTO;

namespace E_commerce.Models.Extentions
{
    public static class CategoryExtensions
    {
        public static Category ToCategory(this CategoryAddRequest categoryAddRequest, IMapper mapper)
        {
            return mapper.Map<Category>(categoryAddRequest);
        }


        public static Category ToCategory(this CategoryUpdateRequest category, IMapper mapper)
        {
            return mapper.Map<Category>(category);

        }


        public static CategoryResponse ToCategoryResponse(this Category category, IMapper mapper)
        {
            return mapper.Map<CategoryResponse>(category);

        }

        public static CategoryUpdateRequest ToCategoryUpdateRequest(this CategoryResponse category, IMapper mapper)
        {
            return mapper.Map<CategoryUpdateRequest>(category);

        }
    }

}
