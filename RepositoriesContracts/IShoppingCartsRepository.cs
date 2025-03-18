using E_commerce.Models;

namespace RepositoriesContracts
{
    public interface IShoppingCartsRepository : IRepository<ShoppingCart>
    {

        public Task<ShoppingCart?> Update(ShoppingCart shoppingCart);

        public Task<double> GetTotalPrice(Guid userId);
        public Task<int> GetCount(Guid userId);

    }
}
