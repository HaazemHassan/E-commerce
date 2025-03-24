using E_commerce.Models.Models;
using System.Linq.Expressions;

namespace E_commerce.UI.ServicesContracts
{
    public interface IOrdersDetailService
    {
        public Task<List<OrderDetail>> GetAll(Expression<Func<OrderDetail, bool>>? filter = null, string? includeProperties = null);
        public Task<OrderDetail> Create(OrderDetail orderDetail);

        public Task<OrderDetail?> Get(int? id);

        //public Task<OrderDetail?> Update(OrderDetail orderDetail);

        public Task<OrderDetail?> Delete(int? id);

    }

}
