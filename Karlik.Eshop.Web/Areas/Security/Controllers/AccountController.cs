using Karlik.Eshop.Web.Controllers;
using Karlik.Eshop.Web.Models.ApplicationServices.Abstraction;
using Karlik.Eshop.Web.Models.Database;
using Karlik.Eshop.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        ISecurityApplicationService security;
        readonly EshopDbContext eshopDbContext;
        public string languageId;

        public AccountController(ISecurityApplicationService _security, EshopDbContext eshopDb, IHttpContextAccessor httpContextAccessor)
        {
            this.security = _security;
            eshopDbContext = eshopDb;
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                languageId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                languageId = "0";
            }
        }

        public IActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.LanguageItems = eshopDbContext.LanguageItems.ToList();

            if (User != null)
            {
                registerViewModel.UserLanguage = eshopDbContext.Users.Where(userId => userId.Id == Convert.ToInt32(languageId)).Select(language => language.LanguageItemID).FirstOrDefault();
            }
            else
            {
                registerViewModel.UserLanguage = Convert.ToInt32(languageId);
            }

            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                string[] errors = await security.Register(registerViewModel, Models.Identity.Roles.Customer);

                if (errors == null)
                {
                    LoginViewModel loginViewModel = new LoginViewModel()
                    {
                        UserName = registerViewModel.UserName,
                        Password = registerViewModel.Password
                    };

                    return await Login(loginViewModel);
                }
            }

            return View(registerViewModel);
        }

        public IActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.LanguageItems = eshopDbContext.LanguageItems.ToList();

            if (User != null)
            {
                loginViewModel.UserLanguage = eshopDbContext.Users.Where(userId => userId.Id == Convert.ToInt32(languageId)).Select(language => language.LanguageItemID).FirstOrDefault();
            }
            else
            {
                loginViewModel.UserLanguage = Convert.ToInt32(languageId);
            }

            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                loginViewModel.LoginFailed = !await security.Login(loginViewModel);
                if (loginViewModel.LoginFailed == false)
                {
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""), new { area = "" });
                }

            }

            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await security.Logout();

            return RedirectToAction(nameof(Login));
        }

    }
}
