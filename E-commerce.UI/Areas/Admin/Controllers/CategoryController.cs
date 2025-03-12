using E_commerce.Models.DTO;
using E_commerce.UI.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Models.Extentions;
using Microsoft.AspNetCore.Authorization;

namespace E_commerce.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CategoryResponse> categories = await _categoriesService.GetAllCategories();
            return View(categories);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddRequest category)
        {
            var response = await _categoriesService.Create(category);
            if (response is null)
            {
                return NotFound();
            }
            TempData["success"] = "Category addedd successfully";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }

            CategoryResponse? category = await _categoriesService.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return View(category.ToCategoryUpdateRequest());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CategoryUpdateRequest category)
        {
            var response = await _categoriesService.UpdateCategory(category);

            if (response is null)
            {
                return BadRequest();
            }
            TempData["success"] = "Category updated successfully";
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }

            CategoryResponse? category = await _categoriesService.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return View(category.ToCategoryUpdateRequest());
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? categoryId)
        {

            await _categoriesService.DeleteCategoryById(categoryId);
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction(nameof(Index));
        }


        //for remote validations
        public async Task<IActionResult> IsCategoryNameNotExists(string name, int? Id)
        {
            CategoryResponse? category = await _categoriesService.GetCategoryByName(name);

            if (category is null)
                return Json(true);

            return Id.HasValue && category.Id == Id ? Json(true) : Json(false);

        }
    }
}
