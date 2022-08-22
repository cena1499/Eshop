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
    public class CarouselController : Controller
    {
        readonly EshopDbContext eshopDbContext;
        IWebHostEnvironment env;
        public string id;

        public CarouselController(EshopDbContext eshopDB, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
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
            CarouselsViewModel carouselsViewModel = new CarouselsViewModel();
            carouselsViewModel.CarouselItems = eshopDbContext.CarouselItems.ToList();
            carouselsViewModel.LanguageItems = eshopDbContext.LanguageItems.ToList();

            if (User != null)
            {
                carouselsViewModel.UserLanguage = eshopDbContext.Users.Where(userId => userId.Id == Convert.ToInt32(id)).Select(language => language.LanguageItemID).FirstOrDefault();
            }
            else
            {
                carouselsViewModel.UserLanguage = Convert.ToInt32(id);
            }

            return View(carouselsViewModel);
        }

        public IActionResult Create()
        {
            CarouselViewModel carouselViewModel = new CarouselViewModel();

            carouselViewModel.LanguageItems = eshopDbContext.LanguageItems.ToList();

            if (User != null)
            {
                carouselViewModel.UserLanguage = eshopDbContext.Users.Where(userId => userId.Id == Convert.ToInt32(id)).Select(language => language.LanguageItemID).FirstOrDefault();
            }
            else
            {
                carouselViewModel.UserLanguage = Convert.ToInt32(id);
            }

            return View(carouselViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarouselItem carouselItem)
        {
            if (carouselItem != null && carouselItem.Image != null)
            {
                FileUpload fileUpload = new FileUpload(env.WebRootPath, "img/CarouselItems", "image");
                carouselItem.ImageSource = await fileUpload.FileUploadAsync(carouselItem.Image);

                ModelState.Clear();
                TryValidateModel(carouselItem);
                if (ModelState.IsValid)
                {
                    if (String.IsNullOrWhiteSpace(carouselItem.ImageSource) == false)
                    {
                        eshopDbContext.CarouselItems.Add(carouselItem);

                        await eshopDbContext.SaveChangesAsync();

                        return RedirectToAction(nameof(CarouselController.Select));
                    }

                }
            }

            return View(carouselItem);

        }

        public IActionResult Edit(int ID)
        {
            CarouselViewModel carouselViewModelFromDb = new CarouselViewModel();
            carouselViewModelFromDb.LanguageItems = eshopDbContext.LanguageItems.ToList();
            carouselViewModelFromDb.CarouselItem = eshopDbContext.CarouselItems.FirstOrDefault(cI => cI.ID == ID);

            if (User != null)
            {
                carouselViewModelFromDb.UserLanguage = eshopDbContext.Users.Where(userId => userId.Id == Convert.ToInt32(id)).Select(language => language.LanguageItemID).FirstOrDefault();
            }
            else
            {
                carouselViewModelFromDb.UserLanguage = Convert.ToInt32(id);
            }

            if (carouselViewModelFromDb != null)
            {
                return View(carouselViewModelFromDb);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarouselItem carouselItem)
        {

            CarouselItem cIFromDB = eshopDbContext.CarouselItems.FirstOrDefault(cI => cI.ID == carouselItem.ID);

            if (cIFromDB != null)
            {
                if (carouselItem != null && carouselItem.Image != null)
                {
                    FileUpload fileUpload = new FileUpload(env.WebRootPath, "img/CarouselItems", "image");
                    carouselItem.ImageSource = await fileUpload.FileUploadAsync(carouselItem.Image);

                    if (String.IsNullOrWhiteSpace(carouselItem.ImageSource) == false)
                    {
                        cIFromDB.ImageSource = carouselItem.ImageSource;

                    }
                }
                else
                {
                    carouselItem.ImageSource = "Fake ImageSource";
                }

                ModelState.Clear();
                TryValidateModel(carouselItem);
                if (ModelState.IsValid)
                {
                    cIFromDB.ImageAlt = carouselItem.ImageAlt;
                    cIFromDB.ImageAltCZ = carouselItem.ImageAltCZ;
                    await eshopDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(CarouselController.Select));
                }

                return View(carouselItem);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int ID)
        {
            DbSet<CarouselItem> carouselItems = eshopDbContext.CarouselItems;

            CarouselItem carouselItem = carouselItems.Where(carouselItem => carouselItem.ID == ID).FirstOrDefault();

            if (carouselItem != null)
            {
                carouselItems.Remove(carouselItem);

                await eshopDbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(CarouselController.Select));
        }



    }
}
