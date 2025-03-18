using AutoMapper;
using E_commerce.Models;
using E_commerce.Models.IdentityEntities;
using E_commerce.UI.ServicesContracts;
using E_commerce_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerce.UI.Areas.Customer.Controllers
{

    [Area(nameof(Customer))]
    [Authorize]
    public class CartController : Controller
    {

        private readonly IShoppingCartsService _cartsService;
        private readonly IApplicationUsersService _usersService;
        private readonly IMapper _mapper;

        public CartController(IShoppingCartsService cartsService, IApplicationUsersService usersService, IMapper mapper)
        {
            _cartsService = cartsService;
            _usersService = usersService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            if (userId is null)
                return BadRequest("User id not found");

            List<ShoppingCart> carts = await _cartsService.GetShoppingCartsByUserId
                (userId, includeProperties: "Product");



            double totalSalary = 0;
            foreach (var cart in carts)
            {
                double priceByQunaity = GetPriceBasedOnQuantity(cart);
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


            ApplicationUser? user = await _usersService.GetApplicationUserById(userId);
            var orderSummaryVM = _mapper.Map<OrderSummaryVM>(user);

            var totalPrice = await _cartsService.GetTotalPrice(userId.Value);
            var cartsCount = await _cartsService.GetCount(userId.Value);

            orderSummaryVM.Price =  totalPrice;
            orderSummaryVM.Count =  cartsCount;

            return View(orderSummaryVM);
        }


        //Helpers
        private double GetPriceBasedOnQuantity(ShoppingCart cart)
        {
            if (cart.Count > 100)
                return cart.Product.Price100;
            if (cart.Count > 50)
                return cart.Product.Price50;
            return cart.Product.Price;


        }

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