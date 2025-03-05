using Microsoft.AspNetCore.Mvc;

namespace E_commerce.UI.Areas.Admin.Controllers
{
    [Controller]
    [Area(nameof(Admin))]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
