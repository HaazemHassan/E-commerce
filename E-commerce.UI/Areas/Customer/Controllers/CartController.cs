using AutoMapper;
using E_commerce.Models;
using E_commerce.Models.Enums;
using E_commerce.Models.IdentityEntities;
using E_commerce.Models.Models;
using E_commerce.UI.ServicesContracts;
using E_commerce_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace E_commerce.UI.Areas.Customer.Controllers
{

    [Area(nameof(Customer))]
    [Authorize]
    public class CartController : Controller
    {

        private readonly IShoppingCartsService _cartsService;
        private readonly IApplicationUsersService _usersService;
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;

        public CartController(IShoppingCartsService cartsService, IApplicationUsersService usersService, IMapper mapper, IOrdersService ordersService)
        {
            _cartsService = cartsService;
            _usersService = usersService;
            _mapper = mapper;
            _ordersService = ordersService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            if (userId is null)
                return BadRequest("User id not found");

            List<ShoppingCart> carts = await _cartsService.GetShoppingCarts
                (userId, includeProperties: nameof(Product));



            double totalSalary = 0;
            foreach (var cart in carts)
            {
                double priceByQunaity = _cartsService.GetPriceBasedOnQuantity(cart);
                cart.Price = priceByQunaity;
                totalSalary += priceByQunaity * cart.Count;
            }

            ViewBag.TotalSalary = totalSalary;
            return View(carts);
        }

        [HttpPost]
        public async Task<IActionResult> IncrementCount(int id)
        {
            var cart = await _cartsService.GetShoppingCartById(id);
            if (cart is null)
                return NotFound("Cart not found");
            cart.Count += 1;
            await _cartsService.UpdateShoppingCart(cart);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> DecrementCount(int id)
        {
            var cart = await _cartsService.GetShoppingCartById(id);
            if (cart is null)
                return NotFound("Cart not found");
            if (cart.Count <= 1)
                await _cartsService.DeleteShoppingCartById(id);
            else
            {
                cart.Count -= 1;
                await _cartsService.UpdateShoppingCart(cart);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var cart = await _cartsService.DeleteShoppingCartById(id);
            if (cart is null)
                return NotFound("Something went wrong while deleting cart");
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            Guid? userId = GetCurrentUserId();
            if (userId is null)
                return BadRequest("User not found");
            int userCarts = await _cartsService.GetCount(userId.Value);
            if (userCarts <= 0)
            {
                return BadRequest("Your cart is empty");
            }


            ApplicationUser? user = await _usersService.GetApplicationUserById(userId);
            var orderSummaryVM = _mapper.Map<OrderSummaryVM>(user);
            orderSummaryVM.ShoppingCartList = await _cartsService.GetShoppingCarts(userId.Value);

            var totalPrice = await _cartsService.GetTotalPrice(userId.Value);
            var cartsCount = await _cartsService.GetCount(userId.Value);

            orderSummaryVM.Price = totalPrice;
            orderSummaryVM.Count = cartsCount;

            return View(orderSummaryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Summary(OrderSummaryVM orderSummary)
        {

            Guid currentUserId = GetCurrentUserId().Value;
            int userCarts = await _cartsService.GetCount(currentUserId);
            if (userCarts <= 0)
            {
                return BadRequest("Your cart is empty");
            }


            // orderSummary.ApplicationUserId = currentUserId;

            Order order = _mapper.Map<Order>(orderSummary);
            order.ApplicationUserId = currentUserId;


            Session? session = await _ordersService.Create(order);
            if (session is not null)
            {
                Response.Headers.Append("Location", session.Url);
                return new StatusCodeResult(303);
            }

            return RedirectToAction(nameof(OrderConfirmation), new { id = order.Id });

        }

        [HttpGet]
        public async Task<IActionResult> OrderConfirmation(int id)
        {
            Order? order = await _ordersService.Get(id);
            if (order is null)
                return NotFound("Order not found");

            if (!order.PaymentStatus.Equals(PaymentStatus.Approved))
            {
                var service = new SessionService();
                Session session = await service.GetAsync(order.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    await _ordersService.UpdateStripePaymentID(id, session.Id, session.PaymentIntentId);
                    await _ordersService.UpdateStatus(id, OrderStatus.Approved, PaymentStatus.Approved);
                }
            }

            List<ShoppingCart> shoppingCarts = await _cartsService.GetShoppingCarts(order.ApplicationUserId);
            await _cartsService.DeleteRange(shoppingCarts);
            return View(id);
        }


        //Helpers
        private Guid? GetCurrentUserId()
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userIdParsed;
            if (Guid.TryParse(userId, out userIdParsed))
                return userIdParsed;
            return null;

        }


    }
}