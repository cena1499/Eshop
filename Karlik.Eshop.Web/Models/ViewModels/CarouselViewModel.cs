using Karlik.Eshop.Web.Models.Database.Entitz;
using Karlik.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Models.ViewModels
{
    public class CarouselViewModel
    {
        public CarouselItem CarouselItem { get; set; }
        public IList<LanguageItem> LanguageItems { get; set; }
        public int UserLanguage { get; set; }
    }
}
