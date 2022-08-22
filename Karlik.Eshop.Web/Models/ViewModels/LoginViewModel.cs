using Karlik.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool LoginFailed { get; set; }
        public IList<LanguageItem> LanguageItems { get; set; }
        public int UserLanguage { get; set; }
    }
}
