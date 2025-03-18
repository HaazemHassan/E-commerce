using E_commerce.Models.Models;

namespace E_commerce.UI.ServicesContracts
{
    public interface IOrdersService
    {
        public Task<List<Order>> GetAll();
        public Task<Order> Create(Order order);

        public Task<Order?> Get(int? id);

        public Task<Order?> Update(Order order);

        public Task<Order?> Delete(int? id);

    }

}
