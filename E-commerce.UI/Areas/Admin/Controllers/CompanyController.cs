using E_commerce.UI.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using E_commerce.Models;

namespace E_commerce.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompaniesService _CompaniesService;

        public CompanyController(ICompaniesService CompaniesService)
        {
            _CompaniesService = CompaniesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Company> Companies = await _CompaniesService.GetAllCompanies();
            return View(Companies);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Company Company)
        {
            var response = await _CompaniesService.Create(Company);
            if (response is null)
            {
                return BadRequest();
            }
            TempData["success"] = "Company addedd successfully";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? companyId)
        {
            if (companyId == null)
            {
                return NotFound();
            }

            Company? company = await _CompaniesService.GetCompanyById(companyId);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Company Company)
        {
            var response = await _CompaniesService.UpdateCompany(Company);

            if (response is null)
            {
                return BadRequest();
            }
            TempData["success"] = "Company updated successfully";
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? companyId)
        {
            if (companyId == null)
            {
                return NotFound();
            }

            Company? Company = await _CompaniesService.GetCompanyById(companyId);

            if (Company == null)
            {
                return BadRequest();
            }

            return View(Company);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? Id)
        {

            await _CompaniesService.DeleteCompanyById(Id);
            TempData["success"] = "Company deleted successfully";

            return RedirectToAction(nameof(Index));
        }


        //for remote validations
        public async Task<IActionResult> IsCompanyNameNotExists(string name, int? Id)
        {
            Company? Company = await _CompaniesService.GetCompanyByName(name);

            if (Company is null)
                return Json(true);

            return Id.HasValue && Company.Id == Id ? Json(true) : Json(false);

        }
    }
}
