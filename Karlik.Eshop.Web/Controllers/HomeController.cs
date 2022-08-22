using Karlik.Eshop.Web.Models;
using Karlik.Eshop.Web.Models.Database;
using Karlik.Eshop.Web.Models.Database.Entitz;
using Karlik.Eshop.Web.Models.Identity;
using Karlik.Eshop.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly EshopDbContext eshopDbContext;
        public string id;

        public HomeController(ILogger<HomeController> logger, EshopDbContext eshopDB, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            eshopDbContext = eshopDB;
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                id = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else 
            {
                id = "0";
            }
        }


        public IActionResult Index()
        {
            _logger.LogInformation("Byla zobrazena hlavni stranka");
            IndexViewModel indexVM = new IndexViewModel();
            indexVM.CarouselItems = eshopDbContext.CarouselItems.ToList();
            indexVM.ProductItems = eshopDbContext.ProductItems.ToList();
            indexVM.LanguageItems = eshopDbContext.LanguageItems.ToList();
            if (User != null)
            {
                indexVM.UserLanguage = eshopDbContext.Users.Where(userId => userId.Id == Convert.ToInt32(id)).Select(language => language.LanguageItemID).FirstOrDefault();
            }
            else
            {
                indexVM.UserLanguage = Convert.ToInt32(id);
            }
            return View(indexVM);
        }

        public IActionResult Privacy()
        {
            PrivacyViewModel privacyViewModel = new PrivacyViewModel();
            privacyViewModel.LanguageItems = eshopDbContext.LanguageItems.ToList();
            if (User != null)
            {
                privacyViewModel.UserLanguage = eshopDbContext.Users.Where(userId => userId.Id == Convert.ToInt32(id)).Select(language => language.LanguageItemID).FirstOrDefault();
            }
            else
            {
                privacyViewModel.UserLanguage = Convert.ToInt32(id);
            }
            return View(privacyViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
