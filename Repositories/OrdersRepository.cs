using E_commerce.DataAccess.Data;
using E_commerce.Models.Enums;
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

            bool isNameTaken = await _db.Orders.AnyAsync(o => o.PersonName == order.PersonName && o.Id != order.Id);
            if (isNameTaken)
                return null;

            _db.Entry(oldOrder).CurrentValues.SetValues(order);
            return order;

        }

        public async Task UpdateStatus(int id, OrderStatus orderStatus, PaymentStatus? paymentStatus)
        {
            var orderFromDb = await _db.Orders.FirstOrDefaultAsync(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus is not null)
                {
                    orderFromDb.PaymentStatus = paymentStatus.Value;
                }
            }
        }

        public async Task UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDb = await _db.Orders.FirstOrDefaultAsync(u => u.Id == id);
            if (orderFromDb is null)
                return;
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromDb.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
            }
        }

    }
}
