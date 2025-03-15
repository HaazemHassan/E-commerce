using E_commerce.Models;
using E_commerce.Models.DTO;
using E_commerce.UI.ServicesContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;


namespace E_commerce.UI.Areas.Customer.Controllers
{
    [Area(nameof(Customer))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductsService _productsService;
        private readonly IShoppingCartsService _shoppingCartsService;

        public HomeController(ILogger<HomeController> logger, IProductsService productsService, IShoppingCartsService shoppingCartsService)
        {
            _logger = logger;
            _productsService = productsService;
            _shoppingCartsService = shoppingCartsService;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<ProductResponse> products = await _productsService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            ProductResponse? product = await _productsService.GetProductById(id, "Category");
            if (product == null)
            {
                return NotFound();
            }
            Product cartProduct = new Product
            {
                Id = product.Id,
                Title = product.Title,
                Author = product.Author,
                ISBN = product.ISBN,
                ListPrice = product.ListPrice,
                Price = product.Price,
                Price50 = product.Price50,
                Price100 = product.Price100,
                Category = product.Category,
                ImageUrl = product.ImageUrl
            };

            ShoppingCart cart = new ShoppingCart
            {
                Product = cartProduct,
                ProductId = cartProduct.Id,
            };

            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(ShoppingCart cart)
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Guid.TryParse(userId, out Guid userIdGuid); 
            cart.ApplicationUserId = userIdGuid;

            ShoppingCart? cartToUpdate = await _shoppingCartsService.GetShoppingCart(cart.ProductId, userIdGuid);

            if (cartToUpdate is null)
                await _shoppingCartsService.Create(cart);
            else
            {
                cartToUpdate.Count += cart.Count;
                await _shoppingCartsService.UpdateShoppingCart(cartToUpdate);

            }
            TempData["success"] = "Cart updated successfully";
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
