using E_commerce.Models.Models;
using E_commerce.UI.ServicesContracts;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace E_commerce.Services
{
    public class StripeService
    {
        private readonly string _secretKey;

        public StripeService(IConfiguration configuration)
        {
            _secretKey = configuration["Stripe:SecretKey"];
            StripeConfiguration.ApiKey = _secretKey;

        }

        public async Task<Session> CreateCheckoutSessionAsync(Order order, List<OrderDetail> orderDetail)
        {
            if (order == null) throw new Exception("Order not found");

            var lineItems = orderDetail.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Product.Title
                    },
                    UnitAmount = (long)(item.Price * 100)   //convert to cents
                },
                Quantity = item.Count
            }).ToList();

            var domain = "https://localhost:7094/";
            var options = new SessionCreateOptions
            {

                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={order.Id}",
                CancelUrl = domain + "customer/cart/index"
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            return session;
        }
    }

}
