using E_commerce.Models;
using E_commerce.Models.DTO;
using E_commerce.UI.ServicesContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace E_commerce.UI.Areas.Customer.Controllers
{
    [Area(nameof(Customer))]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductsService _productsService; 

        public HomeController(ILogger<HomeController> logger, IProductsService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductResponse> products = await _productsService.GetAllProducts();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            ProductResponse? product = await _productsService.GetProductById(id,"Category");
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
