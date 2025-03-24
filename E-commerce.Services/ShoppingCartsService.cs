using E_commerce.Helpers;
using E_commerce.Models;
using E_commerce.UI.ServicesContracts;
using RepositoriesContracts;

namespace E_commerce.UI.Services
{
    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<List<ShoppingCart>> GetAllShoppingCarts()
        {
            return (await _unitOfWork.ShoppingCarts.GetAll()).Select(ShoppingCart => ShoppingCart).ToList();
        }

        public async Task<ShoppingCart> Create(ShoppingCart? shoppingCart)
        {
            ValidationHelper.Validate(shoppingCart);

            ShoppingCart shoppingCartResponse = (await _unitOfWork.ShoppingCarts.Create(shoppingCart!));
            await _unitOfWork.CompleteAsync();
            return shoppingCartResponse;
        }


        public async Task<ShoppingCart?> GetShoppingCartById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            ShoppingCart? shoppingCartResponse = (await _unitOfWork.ShoppingCarts.Get(c => c.Id == id.GetValueOrDefault()));
            return shoppingCartResponse;

        }

        public async Task<ShoppingCart?> GetShoppingCart(int? id, Guid userId)
        {
            return await _unitOfWork.ShoppingCarts.Get(c => c.ProductId == id && c.ApplicationUserId == userId);
        }
        public async Task<List<ShoppingCart>> GetShoppingCarts(Guid userId)
        {
            return await _unitOfWork.ShoppingCarts.GetAll(c => c.ApplicationUserId == userId);
        }

        public async Task<List<ShoppingCart>> GetShoppingCarts(Guid? id, string? includeProperties = null)
        {
            return await _unitOfWork.ShoppingCarts.GetAll(c => c.ApplicationUserId == id, includeProperties);

        }

        public Task<double> GetTotalPrice(Guid userId)
        {
            return _unitOfWork.ShoppingCarts.GetTotalPrice(userId);
        }

        public Task<int> GetCount(Guid userId)
        {
            return _unitOfWork.ShoppingCarts.GetCount(userId);
        }

        public async Task<ShoppingCart?> UpdateShoppingCart(ShoppingCart? shoppingCart)
        {
            ValidationHelper.Validate(shoppingCart);

            var response = await _unitOfWork.ShoppingCarts.Update(shoppingCart!);
            if (response is null)
                return null;
            await _unitOfWork.CompleteAsync();

            return response;

        }

        public async Task<ShoppingCart?> DeleteShoppingCartById(int? id)
        {
            if (id == null)
                return null;
            ShoppingCart? shoppingCart = await _unitOfWork.ShoppingCarts.Get(c => c.Id == id);
            if (shoppingCart is null)
            {
                return null;
            }
            var response = _unitOfWork.ShoppingCarts.Delete(shoppingCart);
            if (response is null)
                return null;

            await _unitOfWork.CompleteAsync();
            return response;
        }

        public double GetPriceBasedOnQuantity(ShoppingCart cart)
        {
            if (cart.Count > 100)
                return cart.Product.Price100;
            if (cart.Count > 50)
                return cart.Product.Price50;
            return cart.Product.Price;

        }

        public async Task DeleteRange(IEnumerable<ShoppingCart> carts)
        {
            _unitOfWork.ShoppingCarts.DeleteRange(carts);
            await _unitOfWork.CompleteAsync();
        }
    }
}
