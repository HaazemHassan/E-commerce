using E_commerce.Models.DTO;
using E_commerce.Models.Enums;
using E_commerce.Models.IdentityEntities;
using E_commerce.UI.ServicesContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoriesContracts;
using System.Threading.Tasks;

namespace E_commerce.UI.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ICompaniesService _companiesService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            ICompaniesService companiesService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _companiesService = companiesService;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            List<SelectListItem> rolesList = new List<SelectListItem>();
            List<SelectListItem> companiesList = new List<SelectListItem>();

            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                rolesList.Add(new SelectListItem(role.ToString(), role.ToString()));
            }

            companiesList = (await _companiesService.GetAllCompanies()).Select(company =>
                new SelectListItem(company.Name, company.Id.ToString())
            ).ToList();

            ViewBag.Roles = rolesList;
            ViewBag.Companies = companiesList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return View(registerDTO);


            if (!Enum.IsDefined(typeof(Roles), registerDTO.Role))
            {
                ModelState.AddModelError("", "Invalid role selected.");
                return View(registerDTO);
            }

            if (!await _roleManager.RoleExistsAsync(registerDTO.Role.ToString()))
            {
                ApplicationRole role = new ApplicationRole { Name = registerDTO.Role.ToString() };
                await _roleManager.CreateAsync(role);
            }


            var user = new ApplicationUser
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                PersonName = registerDTO.PersonName,
                City = registerDTO.City,
                StreetAddress = registerDTO.StreetAddress,
                PostalCode = registerDTO.PostalCode,
                PhoneNumber = registerDTO.PhoneNumber,
             
            };

            if(registerDTO.Role == Roles.Company)
            {
                user.CompanyId = registerDTO.CompanyId;
            }

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, registerDTO.Role.ToString());
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }


            return View(registerDTO);

        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home", new { area = "Customer" });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
        {

            if (!ModelState.IsValid)
                return View(loginDTO);


            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email,
                loginDTO.Password, false, false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    return LocalRedirect(ReturnUrl);
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }


            ModelState.AddModelError("Login", "Invalid email or password");

            return View(loginDTO);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login));

        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        //for remote validations

        [HttpGet]
        public async Task<IActionResult> IsEmailNotExists(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            return user == null ? Json(true) : Json(false);
        }

    }
}
