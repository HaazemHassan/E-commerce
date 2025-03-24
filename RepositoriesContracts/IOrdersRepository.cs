using E_commerce.Models;
using E_commerce.Models.Enums;
using E_commerce.Models.Models;
using System.Linq.Expressions;

namespace RepositoriesContracts
{
    public interface IOrdersRepository : IRepository<Order> 
    {
      
        public Task<Order?> Update(Order order);
        Task UpdateStatus(int id, OrderStatus orderStatus, PaymentStatus? paymentStatus = null);
        Task UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);


    }
}
