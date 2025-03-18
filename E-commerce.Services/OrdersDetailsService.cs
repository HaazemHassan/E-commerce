using E_commerce.Helpers;
using E_commerce.Models.Models;
using E_commerce.UI.ServicesContracts;
using RepositoriesContracts;

namespace E_commerce.UI.Services
{
    public class OrdersDetailService : IOrdersDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<List<OrderDetail>> GetAll()
        {
            return ( await _unitOfWork.OrdersDetail.GetAll()).Select(OrderDetail => OrderDetail).ToList();
        }

        public async Task<OrderDetail> Create(OrderDetail? order)
        {
            ValidationHelper.Validate(order);

            OrderDetail orderResponse = (await _unitOfWork.OrdersDetail.Create(order!));
            await _unitOfWork.CompleteAsync();
            return orderResponse;
        }


        public async Task<OrderDetail?> Get(int? id)
        {
            if (id == null)
            {
                return null;
            }

            OrderDetail? orderResponse = (await _unitOfWork.OrdersDetail.Get(c => c.Id == id.GetValueOrDefault()));
            return orderResponse;

        }

        //public async Task<OrderDetail?> Update(OrderDetail? order)
        //{
        //    ValidationHelper.Validate(order);

        //    var response = await _unitOfWork.OrdersDetail.Update(order!);
        //    if (response is null)
        //        return null;
        //    await _unitOfWork.CompleteAsync();

        //    return response;

        //}

        public async Task<OrderDetail?> Delete(int? id)
        {
            if (id == null)
                return null;
            OrderDetail? order = await _unitOfWork.OrdersDetail.Get(c => c.Id == id);
            if (order is null)
            {
                return null;
            }
            var response = _unitOfWork.OrdersDetail.Delete(order);
            if (response is null)
                return null;

            await _unitOfWork.CompleteAsync();
            return response;
        }
    }
}
