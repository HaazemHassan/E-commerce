﻿using E_commerce.Models;

namespace E_commerce.UI.ServicesContracts
{
    public interface IShoppingCartsService
    {
        public Task<List<ShoppingCart>> GetAllShoppingCarts();
        public Task<ShoppingCart> Create(ShoppingCart shoppingCart);

        public Task<ShoppingCart?> GetShoppingCartById(int? id);
        public Task<ShoppingCart?> GetShoppingCart(int? productId, Guid userId);
        public Task<List<ShoppingCart>> GetShoppingCarts(Guid? id, string? includeProperties = null);
        public Task<double> GetTotalPrice(Guid userId);
        public Task<int> GetCount(Guid userId);
        public Task<ShoppingCart?> UpdateShoppingCart(ShoppingCart shoppingCart);
        public Task<ShoppingCart?> DeleteShoppingCartById(int? id);
        public Task DeleteRange(IEnumerable<ShoppingCart> carts);
        public double GetPriceBasedOnQuantity(ShoppingCart cart);


    }
}
