using Karlik.Eshop.Web.Models.Database.Entitz;
using Karlik.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Models.Database
{
    public static class DatabaseFake
    {
        public static List<CarouselItem> CarouselItems { get; set; }
        public static List<ProductItem> ProductItems { get; set; }
        public static List<LanguageItem> LanguageItems { get; set; }

        static DatabaseFake()
        {
            DatabaseInnit dvInit = new DatabaseInnit();
            CarouselItems = dvInit.GenerateCarouselItems();
            ProductItems = dvInit.GenerateProductItems();
            LanguageItems = dvInit.GenerateLanguageItems();
        }
    }
}
