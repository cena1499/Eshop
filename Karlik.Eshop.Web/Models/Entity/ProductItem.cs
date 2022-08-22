using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Models.Entity
{
    [Table(nameof(ProductItem))]
    public class ProductItem
    {
        [Key]
        [Required]
        public int ID { get; set; }
        
        [StringLength(20)]
        public string SKU { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string ProductNameCZ { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        
        [Range(0, double.MaxValue)]
        public double Sale { get; set; }
        
        [StringLength(1024)]
        public string Detail { get; set; }

        [StringLength(1024)]
        public string DetailCZ { get; set; }

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
