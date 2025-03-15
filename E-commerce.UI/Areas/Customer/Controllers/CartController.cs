using E_commerce.Models;
using E_commerce.UI.ServicesContracts;
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

        public CartController(IShoppingCartsService cartsService)
        {
            _cartsService = cartsService;
        }
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<ShoppingCart> carts = await _cartsService.GetShoppingCartsByUserId
                (Guid.Parse(userId!), includeProperties: nameof(Product));

            

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
        public IActionResult Summary()
        {
            return View();
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


    }
}