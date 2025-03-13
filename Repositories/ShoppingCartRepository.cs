using E_commerce.DataAccess.Data;
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using RepositoriesContracts;

namespace Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartsRepository
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ShoppingCart?> Update(ShoppingCart cart)
        {
            ShoppingCart? oldShoppingCart = await _db.ShoppingCarts.FirstOrDefaultAsync(c => c.Id == cart.Id);
            if (oldShoppingCart == null)
                return null;


            _db.Entry(oldShoppingCart).CurrentValues.SetValues(cart);
            return cart;

        }

    }
}
