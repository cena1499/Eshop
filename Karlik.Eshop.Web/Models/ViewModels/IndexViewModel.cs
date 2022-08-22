using Karlik.Eshop.Web.Models.Database.Entitz;
using Karlik.Eshop.Web.Models.Entity;
using Karlik.Eshop.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Models.ViewModels
{
    public class IndexViewModel
    {
        public IList<CarouselItem> CarouselItems { get; set; }
        public IList<ProductItem> ProductItems { get; set; }
        public IList<LanguageItem> LanguageItems { get; set; }
        public int UserLanguage { get; set; }
    }
}
