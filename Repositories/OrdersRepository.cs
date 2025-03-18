using E_commerce.DataAccess.Data;
using E_commerce.Models.Models;
using Microsoft.EntityFrameworkCore;
using RepositoriesContracts;

namespace Repositories
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        private readonly ApplicationDbContext _db;

        public OrdersRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Order?> Update(Order order)
        {
            Order? oldOrder = await _db.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);
            if (oldOrder == null)
                return null;

            bool isNameTaken = await _db.Orders.AnyAsync(o => o.Name == order.Name && o.Id != order.Id);
            if (isNameTaken)
                return null;

            _db.Entry(oldOrder).CurrentValues.SetValues(order);
            return order;

        }

    }
}
