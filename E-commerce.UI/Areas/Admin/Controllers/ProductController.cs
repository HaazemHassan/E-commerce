using E_commerce.Models.DTO;
using E_commerce.Models.Extentions;
using E_commerce.Models.Models;
using E_commerce.UI.ServicesContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.UI.Areas.Admin.Controllers
{
    [Controller]
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]
    
    public class ProductController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductsService productService, ICategoriesService categoriesService, IWebHostEnvironment webHostEnvironment)
        {
            _productsService = productService;
            _categoriesService = categoriesService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            List<ProductResponse> products = (await _productsService.GetAllProducts(nameof(ProductResponse.Category)));

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<SelectListItem> categoriesList = (await _categoriesService.GetAllCategories()).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            ViewBag.Categories = categoriesList;


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductAddRequest product, IFormFile? file)
        {

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file is not null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\products");

                using (var fileSteam = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileSteam);
                }
                product.ImageUrl = @"\images\products\" + fileName;
            }
            var response = await _productsService.Create(product);
            if (response is null)
            {
                return BadRequest();
            }
            TempData["success"] = "Product addedd successfully";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            ProductResponse? product = await _productsService.GetProductById(productId);

            if (product == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> categoriesList = (await _categoriesService.GetAllCategories()).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            ViewBag.Categories = categoriesList;

            return View(product.ToProductUpdateRequest());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductUpdateRequest product, IFormFile? file)
        {

            string wwwRootPath = _webHostEnvironment.WebRootPath;

            //we 
            if (file is not null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\products");

                using (var fileSteam = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileSteam);
                }

                //we should delete the old image
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    string oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                product.ImageUrl = @"\images\products\" + fileName;

            }
            var response = await _productsService.UpdateProduct(product);

            if (response is null)
            {
                return BadRequest();
            }
            TempData["success"] = "Product updated successfully";
            return RedirectToAction(nameof(Index));
        }



        //[HttpGet]
        //public async Task<IActionResult> Delete(int? productId)
        //{
        //    if (productId == null)
        //    {
        //        return NotFound();
        //    }

        //    ProductResponse? product = await _productsService.GetProductById(productId);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product.ToProductUpdateRequest());
        //}


        //public async Task<IActionResult> DeletePost(int? productId)
        //{
        //    ProductResponse? product = await _productsService.GetProductById(productId);
        //    if (product is null)
        //        return NotFound();

        //    string wwwRootPath = _webHostEnvironment.WebRootPath;


        //    //we should delete the old image
        //    if (!string.IsNullOrEmpty(product.ImageUrl))
        //    {
        //        string ImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
        //        if (System.IO.File.Exists(ImagePath))
        //        {
        //            System.IO.File.Delete(ImagePath);
        //        }
        //    }

        //    ProductResponse? deletedProduct = await _productsService.DeleteProductById(productId);

        //    if (deletedProduct is null)
        //        return NotFound();
        //    TempData["success"] = "Product deleted successfully";

        //    return RedirectToAction(nameof(Index));
        //}

        #region used for remote validation
        public async Task<IActionResult> IsProductISBNNotExists(string ISBN, int? Id)
        {
            ProductResponse? product = await _productsService.GetProductByISBN(ISBN);

            if (product is null)
                return Json(true);

            return Id.HasValue && product.Id == Id ? Json(true) : Json(false);

        }
        #endregion

        #region Api calls

        public async Task<IActionResult> GetAll()
        {
            List<ProductResponse> products = (await _productsService.GetAllProducts(nameof(ProductResponse.Category)));
            return Json(new { data = products });

        }

        [HttpDelete]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {

            ProductResponse? product = await _productsService.GetProductById(id);
            if (product is null)
                return Json(new { sucess = "false", message = "Product doesn't exist" });

            string wwwRootPath = _webHostEnvironment.WebRootPath;


            //we should delete the image
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                string ImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(ImagePath))
                {
                    System.IO.File.Delete(ImagePath);
                }
            }

            ProductResponse? deletedProduct = await _productsService.DeleteProductById(id);

            if (deletedProduct is null)
                return Json(new { sucess = "false", message = "Something went wrong" });

            return Json(new { sucess = "true", message = "Product deleted successfully" });

        }

        #endregion
    }
}
