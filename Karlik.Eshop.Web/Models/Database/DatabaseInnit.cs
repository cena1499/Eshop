using Karlik.Eshop.Web.Models.Database.Entitz;
using Karlik.Eshop.Web.Models.Entity;
using Karlik.Eshop.Web.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Models.Database
{
    public class DatabaseInnit
    {
        public async Task EnsureRoleCreated(RoleManager<Role> roleManager)
        {
            string[] roles = Enum.GetNames(typeof(Roles));

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new Role(role));
            }
        }

        public async Task EnsureAdminCreated(UserManager<User> userManager)
        {
            User user = new User
            {
                UserName = "admin",
                Email = "admin@admin.cz",
                EmailConfirmed = true,
                FirstName = "jmeno",
                LastName = "prijmeni",
                LanguageItemID = 0
            };
            string password = "abc";

            User adminInDatabase = await userManager.FindByNameAsync(user.UserName);

            if (adminInDatabase == null)
            {

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result == IdentityResult.Success)
                {
                    string[] roles = Enum.GetNames(typeof(Roles));
                    foreach (var role in roles)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
                else if (result != null && result.Errors != null && result.Errors.Count() > 0)
                {
                    foreach (var error in result.Errors)
                    {
                        Debug.WriteLine($"Error during Role creation for Admin: {error.Code}, {error.Description}");
                    }
                }
            }

        }

        public async Task EnsureManagerCreated(UserManager<User> userManager)
        {
            User user = new User
            {
                UserName = "manager",
                Email = "manager@manager.cz",
                EmailConfirmed = true,
                FirstName = "jmeno",
                LastName = "prijmeni",
                LanguageItemID = 0
            };
            string password = "abc";

            User managerInDatabase = await userManager.FindByNameAsync(user.UserName);

            if (managerInDatabase == null)
            {

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result == IdentityResult.Success)
                {
                    string[] roles = Enum.GetNames(typeof(Roles));
                    foreach (var role in roles)
                    {
                        if (role != Roles.Admin.ToString())
                            await userManager.AddToRoleAsync(user, role);
                    }
                }
                else if (result != null && result.Errors != null && result.Errors.Count() > 0)
                {
                    foreach (var error in result.Errors)
                    {
                        Debug.WriteLine($"Error during Role creation for Manager: {error.Code}, {error.Description}");
                    }
                }
            }

        }

        public void Initialize(EshopDbContext eshopDbContext)
        {
            eshopDbContext.Database.EnsureCreated();

            if (eshopDbContext.CarouselItems.Count() == 0)
            {
                IList<CarouselItem> carouselItems = GenerateCarouselItems();
                foreach (var ci in carouselItems)
                {
                    eshopDbContext.CarouselItems.Add(ci);
                }
                eshopDbContext.SaveChanges();
            }

            if (eshopDbContext.ProductItems.Count() == 0)
            {
                IList<ProductItem> productItems = GenerateProductItems();
                foreach (var pi in productItems)
                {
                    eshopDbContext.ProductItems.Add(pi);
                }
                eshopDbContext.SaveChanges();
            }

            if (eshopDbContext.LanguageItems.Count() == 0)
            {
                IList<LanguageItem> languageItems = GenerateLanguageItems();
                foreach (var li in languageItems)
                {
                    eshopDbContext.LanguageItems.Add(li);
                }
                eshopDbContext.SaveChanges();
            }
        }

        public List<LanguageItem> GenerateLanguageItems()
        {
            List<LanguageItem> languageItems = new List<LanguageItem>();

            LanguageItem english = new LanguageItem()
            {
                ID = 0,
                LanguageName = "English",
                FlagIconName = "flag-united-kingdom"
            };

            LanguageItem czech = new LanguageItem()
            {
                ID = 1,
                LanguageName = "Čeština",
                FlagIconName = "flag-czech-republic"
            };

            languageItems.Add(english);
            languageItems.Add(czech);

            return languageItems;
        }

        public List<CarouselItem> GenerateCarouselItems()
        {
            List<CarouselItem> carouselItems = new List<CarouselItem>();
            
            CarouselItem ci1 = new CarouselItem()
            {
                ID = 0,
                ImageSource = "/img/it1.jpg",
                ImageAlt = "First Slide",
                ImageAltCZ = "CZ ALT"
            };          

            CarouselItem ci2 = new CarouselItem()
            {
                ID = 1,
                ImageSource = "/img/it2.jpg",
                ImageAlt = "Second Slide",
                ImageAltCZ = "CZ ALT"
            };

            CarouselItem ci3 = new CarouselItem()
            {
                ID = 2,
                ImageSource = "/img/it3.jpg",
                ImageAlt = "Third Slide",
                ImageAltCZ = "CZ ALT"
            };

            carouselItems.Add(ci1);
            carouselItems.Add(ci2);
            carouselItems.Add(ci3);

            return carouselItems;
        }

        public List<ProductItem> GenerateProductItems()
        {
            List<ProductItem> productItems = new List<ProductItem>();

            ProductItem pi1 = new ProductItem()
            {
                ID = 0,
                SKU = "A1-3",
                Price = 10.5,
                Sale = 10,
                Detail = "some detail......",
                ProductName = "Car",
                ImageSource = "/img/product1.jpg",
                ImageAlt = "Product 1",
                ProductNameCZ = "Auto",
                DetailCZ = "nejaky detail...",
                ImageAltCZ = "Produkt auto"
            };

            ProductItem pi2 = new ProductItem()
            {
                ID = 1,
                SKU = "A1-3",
                Price = 10.5,
                Sale = 10,
                Detail = "some detail......",
                ProductName = "Car",
                ImageSource = "/img/product1.jpg",
                ImageAlt = "Product 1",
                ProductNameCZ = "Auto",
                DetailCZ = "nejaky detail...",
                ImageAltCZ = "Produkt auto"
            };

            ProductItem pi3 = new ProductItem()
            {
                ID = 2,
                SKU = "A1-3",
                Price = 10.5,
                Sale = 10,
                Detail = "some detail......",
                ProductName = "Car",
                ImageSource = "/img/product1.jpg",
                ImageAlt = "Product 1",
                ProductNameCZ = "Auto",
                DetailCZ = "nejaky detail...",
                ImageAltCZ = "Produkt auto"
            };

            productItems.Add(pi1);
            productItems.Add(pi2);
            productItems.Add(pi3);

            return productItems;
        }
    }
}
