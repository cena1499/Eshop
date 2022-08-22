using Karlik.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Models.ViewModels
{
    public class ProductViewModel
    {
        public ProductItem ProductItem { get; set; }
        public IList<LanguageItem> LanguageItems { get; set; }
        public int UserLanguage { get; set; }
    }
}
