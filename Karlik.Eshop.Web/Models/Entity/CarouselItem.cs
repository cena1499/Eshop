using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Karlik.Eshop.Web.Models.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Models.Database.Entitz
{
    [Table(nameof(CarouselItem))]
    public class CarouselItem
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [ContentType("image")]
        [NotMapped]
        public IFormFile Image { get; set; }

        [StringLength(255)]
        [Required]
        public string ImageSource { get; set; }
        [StringLength(50)]
        public string ImageAlt { get; set; }
        [StringLength(50)]
        public string ImageAltCZ { get; set; }
    }
}
