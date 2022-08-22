using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Karlik.Eshop.Web.Models.Entity
{
    [Table(nameof(LanguageItem))]
    public class LanguageItem
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [StringLength(50)]
        [Required]
        public string LanguageName { get; set; }

        [StringLength(20)]
        [Required]
        public string FlagIconName { get; set; }
    }
}
