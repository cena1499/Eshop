using Karlik.Eshop.Web.Models.Database;
using Karlik.Eshop.Web.Models.Entity;
using Karlik.Eshop.Web.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Controllers
{
    public class ProductController : Controller
    {

        readonly EshopDbContext eshopDbContext;
        IWebHostEnvironment env;
        public string languageId;

        public ProductController(EshopDbContext eshopDb, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            eshopDbContext = eshopDb;
            this.env = env;

            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                languageId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                languageId = "0";
            }
        }

        public IActionResult Detail(int id)
        {
            DbSet<ProductItem> productItems = eshopDbContext.ProductItems;

            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.ProductItem = productItems.Where(productItem => productItem.ID == id).FirstOrDefault();
            productViewModel.LanguageItems = eshopDbContext.LanguageItems.ToList();

            if (User != null)
            {
                productViewModel.UserLanguage = eshopDbContext.Users.Where(userId => userId.Id == Convert.ToInt32(languageId)).Select(language => language.LanguageItemID).FirstOrDefault();
            }
            else
            {
                productViewModel.UserLanguage = Convert.ToInt32(languageId);
            }

            return View(productViewModel);
        }
    }
}
