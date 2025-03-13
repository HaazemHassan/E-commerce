using E_commerce.Models;

namespace E_commerce.UI.ServicesContracts
{
    public interface IShoppingCartsService
    {
        public Task<List<ShoppingCart>> GetAllShoppingCarts();
        public Task<ShoppingCart> Create(ShoppingCart shoppingCart);

        public Task<ShoppingCart?> GetShoppingCartById(int? id);

        public Task<ShoppingCart?> UpdateShoppingCart(ShoppingCart shoppingCart);

        public Task<ShoppingCart?> DeleteShoppingCartById(int? id);

    }
}
