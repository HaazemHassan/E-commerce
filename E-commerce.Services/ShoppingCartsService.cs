using E_commerce.Helpers;
using E_commerce.Models;
using E_commerce.UI.ServicesContracts;
using RepositoriesContracts;

namespace E_commerce.UI.Services
{
    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartsService(IUnitOfWork categoriesRepository)
        {
            _unitOfWork = categoriesRepository;
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

        public async Task<ShoppingCart?> GetShoppingCart(int? id,Guid userId)
        {
            return await _unitOfWork.ShoppingCarts.Get(c => c.ProductId == id && c.ApplicationUserId == userId);
        }

        public async Task<List<ShoppingCart>> GetShoppingCartsByUserId(Guid? id,string? includeProperties = null)
        {
            return await _unitOfWork.ShoppingCarts.GetAll(c => c.ApplicationUserId == id,includeProperties);

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
    }
}
