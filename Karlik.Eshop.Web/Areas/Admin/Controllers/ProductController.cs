using Karlik.Eshop.Web.Models.Database;
using Karlik.Eshop.Web.Models.Database.Entitz;
using Karlik.Eshop.Web.Models.Entity;
using Karlik.Eshop.Web.Models.Identity;
using Karlik.Eshop.Web.Models.Implementation;
using Karlik.Eshop.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class ProductController : Controller
    {
        readonly EshopDbContext eshopDbContext;
        IWebHostEnvironment env;
        public string id;

        public ProductController(EshopDbContext eshopDB, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            eshopDbContext = eshopDB;
            this.env = env;

            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                id = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                id = "0";
            }
        }
        public IActionResult Select()
        {
            ProductsViewModel productsViewModel = new ProductsViewModel();
            productsViewModel.ProductItems = eshopDbContext.ProductItems.ToList();
            productsViewModel.LanguageItems = eshopDbContext.LanguageItems.ToList();

            if (User != null)
            {
                productsViewModel.UserLanguage = eshopDbContext.Users.Where(userId => userId.Id == Convert.ToInt32(id)).Select(language => language.LanguageItemID).FirstOrDefault();
            }
            else
            {
                productsViewModel.UserLanguage = Convert.ToInt32(id);
            }

            return View(productsViewModel);
        }

        public IActionResult Create()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.LanguageItems = eshopDbContext.LanguageItems.ToList();

            if (User != null)
            {
                productViewModel.UserLanguage = eshopDbContext.Users.Where(userId => userId.Id == Convert.ToInt32(id)).Select(language => language.LanguageItemID).FirstOrDefault();
            }
            else
            {
                productViewModel.UserLanguage = Convert.ToInt32(id);
            }

            return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            
            if (product != null && product.ProductItem.Image != null)
            {
                FileUpload fileUpload = new FileUpload(env.WebRootPath, "img/Products", "image");
                product.ProductItem.ImageSource = await fileUpload.FileUploadAsync(product.ProductItem.Image);

                if (String.IsNullOrWhiteSpace(product.ProductItem.ImageSource) == false)
                {
                    eshopDbContext.ProductItems.Add(product.ProductItem);

                    await eshopDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(ProductController.Select));
                }
            }

            return View(product);

        }

        public IActionResult Edit(int ID)
        {
            ProductViewModel productViewModelFromDb = new ProductViewModel();
            productViewModelFromDb.LanguageItems = eshopDbContext.LanguageItems.ToList();
            productViewModelFromDb.ProductItem = eshopDbContext.ProductItems.FirstOrDefault(cI => cI.ID == ID);

            if (User != null)
            {
                productViewModelFromDb.UserLanguage = eshopDbContext.Users.Where(userId => userId.Id == Convert.ToInt32(id)).Select(language => language.LanguageItemID).FirstOrDefault();
            }
            else
            {
                productViewModelFromDb.UserLanguage = Convert.ToInt32(id);
            }

            if (productViewModelFromDb != null)
            {
                return View(productViewModelFromDb);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel product)
        {

            ProductItem productFromDB = eshopDbContext.ProductItems.FirstOrDefault(cI => cI.ID == product.ProductItem.ID);

            if (productFromDB != null)
            {
                if (product != null && product.ProductItem.Image != null)
                {
                    FileUpload fileUpload = new FileUpload(env.WebRootPath, "img/Products", "image");
                    product.ProductItem.ImageSource = await fileUpload.FileUploadAsync(product.ProductItem.Image);

                    if (String.IsNullOrWhiteSpace(product.ProductItem.ImageSource) == false)
                    {
                        productFromDB.ImageSource = product.ProductItem.ImageSource;

                    }
                }
                productFromDB.ProductName = product.ProductItem.ProductName;
                productFromDB.Price = product.ProductItem.Price;
                productFromDB.Sale = product.ProductItem.Sale;
                productFromDB.SKU = product.ProductItem.SKU;
                productFromDB.Detail = product.ProductItem.Detail;
                productFromDB.ImageAlt = product.ProductItem.ImageAlt;
                productFromDB.ProductNameCZ = product.ProductItem.ProductNameCZ;
                productFromDB.DetailCZ = product.ProductItem.DetailCZ;
                productFromDB.ImageAltCZ = product.ProductItem.ImageAltCZ;
                await eshopDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(ProductController.Select));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int ID)
        {
            DbSet<ProductItem> products = eshopDbContext.ProductItems;

            ProductItem product = products.Where(carouselItem => carouselItem.ID == ID).FirstOrDefault();

            if (product != null)
            {
                products.Remove(product);

                await eshopDbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ProductController.Select));
        }
    }
}
