using E_commerce.Helpers;
using E_commerce.Models.Models;
using E_commerce.UI.ServicesContracts;
using RepositoriesContracts;

namespace E_commerce.UI.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<List<Order>> GetAll()
        {
            return (await _unitOfWork.Orders.GetAll()).Select(Order => Order).ToList();
        }

        public async Task<Order> Create(Order? order)
        {
            ValidationHelper.Validate(order);

            Order orderResponse = (await _unitOfWork.Orders.Create(order!));
            await _unitOfWork.CompleteAsync();
            return orderResponse;
        }



        public async Task<Order?> Get(int? id)
        {
            if (id == null)
            {
                return null;
            }

            Order? orderResponse = (await _unitOfWork.Orders.Get(c => c.Id == id.GetValueOrDefault()));
            return orderResponse;

        }

        public async Task<Order?> Update(Order? order)
        {
            ValidationHelper.Validate(order);

            var response = await _unitOfWork.Orders.Update(order!);
            if (response is null)
                return null;
            await _unitOfWork.CompleteAsync();

            return response;

        }

        public async Task<Order?> Delete(int? id)
        {
            if (id == null)
                return null;
            Order? order = await _unitOfWork.Orders.Get(c => c.Id == id);
            if (order is null)
            {
                return null;
            }
            var response = _unitOfWork.Orders.Delete(order);
            if (response is null)
                return null;

            await _unitOfWork.CompleteAsync();
            return response;
        }
    }
}
