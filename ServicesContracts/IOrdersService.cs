using E_commerce.Models.Enums;
using E_commerce.Models.Models;
using Stripe.Checkout;

namespace E_commerce.UI.ServicesContracts
{
    public interface IOrdersService
    {
        public Task<List<Order>> GetAll();
        public Task<Session?> Create(Order order);

        public Task<Order?> Get(int? id);

        public Task<Order?> Update(Order order);

        public Task<Order?> Delete(int? id);
        Task UpdateStatus(int id, OrderStatus orderStatus, PaymentStatus? paymentStatus = null);
        Task UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);

    }

}
