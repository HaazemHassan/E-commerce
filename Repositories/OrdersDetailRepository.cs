using E_commerce.DataAccess.Data;
using E_commerce.Models;
using E_commerce.Models.Models;
using Microsoft.EntityFrameworkCore;
using RepositoriesContracts;

namespace Repositories
{
    public class OrdersDetailRepository : Repository<OrderDetail>, IOrdersDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public OrdersDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public async Task<OrderDetail?> Update(OrderDetail orderDetail)
        //{
        //    OrderDetail? oldOrderDetail = await _db.OrderDetails.FirstOrDefaultAsync(c => c.Id == orderDetail.Id);
        //    if (oldOrderDetail == null)
        //        return null;

        //    bool isNameTaken = await _db.OrderDetails.AnyAsync(c => c.Name == orderDetail.Name && c.Id != orderDetail.Id);
        //    if (isNameTaken)
        //        return null;
        //        //throw new ArgumentException("OrderDetail name is already taken by another orderDetail");


        //    //better than _db.Update(orderDetail)
        //    //because this will update only the changed vaues
        //    _db.Entry(oldOrderDetail).CurrentValues.SetValues(orderDetail);
        //    return orderDetail;

        //}

    }
}
