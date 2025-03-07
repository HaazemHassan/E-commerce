using E_commerce.Models.DTO;
using E_commerce.Models.Models;

namespace E_commerce.UI.ServicesContracts
{
    public interface IProductsService
    {
        public Task<List<ProductResponse>> GetAllProducts(string? includeProperties = null);
        public Task<ProductResponse?> Create(ProductAddRequest product);

        public Task<ProductResponse?> GetProductById(int? id, string? includeProperties = null);


        //should be ProductUpdateRequest but ok :)
        public Task<ProductResponse?> UpdateProduct(ProductUpdateRequest product);

        public Task<ProductResponse?> DeleteProductById(int? id);
        public Task<ProductResponse?> GetProductByTitle(string name, string? includeProperties = null);
        public Task<ProductResponse?> GetProductByISBN(string ISBN, string? includeProperties = null);

    }
}
