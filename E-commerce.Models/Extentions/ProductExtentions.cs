using E_commerce.Models.DTO;

namespace E_commerce.Models.Extentions
{
    public static class ProductExtensions
    {
        public static Product ToProduct(this ProductAddRequest productAddRequest)
        {
            return new Product
            {
                Author = productAddRequest.Author,
                Description = productAddRequest.Description,
                ISBN = productAddRequest.ISBN,
                ListPrice = productAddRequest.ListPrice,
                Price = productAddRequest.Price,
                Price100 = productAddRequest.Price100,
                Price50 = productAddRequest.Price50,
                Title = productAddRequest.Title,
                Category = productAddRequest.Category,
                CategoryId = productAddRequest.CategoryId,
                ImageUrl = productAddRequest.ImageUrl,
            };
        }


        public static Product ToProduct(this ProductUpdateRequest product)
        {
            return new Product
            {
                Id = product.Id,
                Author = product.Author,
                Description = product.Description,
                Title = product.Title,
                ListPrice = product.ListPrice,
                Price = product.Price,
                Price100 = product.Price100,
                Price50 = product.Price50,
                ISBN = product.ISBN,
                Category = product.Category,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,

            };
        }


        public static ProductResponse ToProductResponse(this Product product)
        {
            return new ProductResponse
            {
                Title = product.Title,
                Author = product.Author,
                Description = product.Description,
                ISBN = product.ISBN,
                ListPrice = product.ListPrice,
                Id = product.Id,
                Price = product.Price,
                Price100 = product.Price100,
                Price50 = product.Price50,
                Category = product.Category,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,


            };
        }

        public static ProductUpdateRequest ToProductUpdateRequest(this ProductResponse product)
        {
            return new ProductUpdateRequest
            {
                Id = product.Id,
                Title = product.Title,
                Author = product.Author,
                Description = product.Description,
                ISBN = product.ISBN,
                ListPrice = product.ListPrice,
                Price = product.Price,
                Price100 = product.Price100,
                Price50 = product.Price50,
                Category = product.Category,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
            };
        }
    }

}
