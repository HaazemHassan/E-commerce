using E_commerce.Models;

namespace RepositoriesContracts
{
    public interface IShoppingCartsRepository : IRepository<ShoppingCart>
    {

        public Task<ShoppingCart?> Update(ShoppingCart shoppingCart);


    }
}
