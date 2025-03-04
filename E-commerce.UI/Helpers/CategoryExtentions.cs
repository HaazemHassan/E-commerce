using E_commerce.UI.DTO;
using E_commerce.UI.Models;

namespace E_commerce.UI.Helpers
{
    public static class CategoryExtensions
    {
        public static Category ToCategory(this CategoryAddRequest categoryAddRequest)
        {
            return new Category
            {
                Name = categoryAddRequest.Name,
                DisplayOrder = categoryAddRequest.DisplayOrder
            };
        }


        public static Category ToCategory(this CategoryUpdateRequest category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder
            };
        }


        public static CategoryResponse ToCategoryResponse(this Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder
            };
        }

        public static CategoryUpdateRequest ToCategoryUpdateRequest(this CategoryResponse category)
        {
            return new CategoryUpdateRequest
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder
            };
        }
    }

}
