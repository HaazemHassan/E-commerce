using E_commerce.Models;

namespace RepositoriesContracts
{
    public interface IProductsRepository : IRepository<Product>
    {
        public Task<Product?> Update(Product product);

    }
}
