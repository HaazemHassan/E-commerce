using AutoMapper;
using E_commerce.Helpers;
using E_commerce.Models;
using E_commerce.Models.Enums;
using E_commerce.Models.Models;
using E_commerce.Services;
using E_commerce.UI.ServicesContracts;
using RepositoriesContracts;
using Stripe.Checkout;


namespace E_commerce.UI.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShoppingCartsService _cartsService;
        private readonly IMapper _mapper;
        private readonly StripeService _stripeService;

        public OrdersService(IUnitOfWork unitOfWork, IShoppingCartsService cartsService,
            IMapper mapper, StripeService stripeService)
        {
            _unitOfWork = unitOfWork;
            _cartsService = cartsService;
            _mapper = mapper;
            _stripeService = stripeService;
        }


        public async Task<List<Order>> GetAll()
        {
            return (await _unitOfWork.Orders.GetAll()).Select(Order => Order).ToList();
        }

        public async Task<Session?> Create(Order? order)
        {
            ValidationHelper.Validate(order);
            Session? session = null;

            var user = await _unitOfWork.ApplicationUsers.Get(u => u.Id == order!.ApplicationUserId);

            if (user!.CompanyId != null)
            {
                //it's a company user - Payment are delayed
                order.OrderStatus = OrderStatus.Approved;
                order.PaymentStatus = PaymentStatus.ApprovedForDelayedPayment;
                order.PaymentDueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30));
            }

            order.Price = await _cartsService.GetTotalPrice(user.Id);

            await _unitOfWork.Orders.Create(order!);
            List<ShoppingCart> carts = await _cartsService.GetShoppingCarts(user.Id, nameof(Product));

            await _unitOfWork.CompleteAsync();

            foreach (var cart in carts)
            {
                OrderDetail orderDetail = _mapper.Map<OrderDetail>(cart);
                orderDetail.OrderId = order.Id;
                orderDetail.Price = _cartsService.GetPriceBasedOnQuantity(cart);
                orderDetail.Product = null;
                await _unitOfWork.OrdersDetail.Create(orderDetail);
            }
            await _unitOfWork.CompleteAsync();


            if (user.CompanyId == null)
            {
                List<OrderDetail> ordersDetail = await _unitOfWork.OrdersDetail.GetAll(od => od.OrderId == order.Id, nameof(Product));
                session = await _stripeService.CreateCheckoutSessionAsync(order, ordersDetail);

                await UpdateStripePaymentID(order.Id, session.Id, session.PaymentIntentId);


            }

            await _unitOfWork.CompleteAsync();

            return session;
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

        public async Task UpdateStatus(int id, OrderStatus orderStatus, PaymentStatus? paymentStatus = null)
        {
            await _unitOfWork.Orders.UpdateStatus(id, orderStatus, paymentStatus);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateStripePaymentID(int id, string sessionId, string? paymentIntentId)
        {
            await _unitOfWork.Orders.UpdateStripePaymentID(id, sessionId, paymentIntentId);
            await _unitOfWork.CompleteAsync();
        }


    }
}
