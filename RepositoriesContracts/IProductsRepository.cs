using E_commerce.Models;
using E_commerce.Models.Models;

namespace RepositoriesContracts
{
    public interface IProductsRepository : IRepository<Product>
    {
        public Task<Product?> UpdateProduct(Product product);

    }
}
