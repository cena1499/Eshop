using Karlik.Eshop.Web.Models.Database.Configuration;
using Karlik.Eshop.Web.Models.Database.Entitz;
using Karlik.Eshop.Web.Models.Entity;
using Karlik.Eshop.Web.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web.Models.Database
{
    public class EshopDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<CarouselItem> CarouselItems { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<LanguageItem> LanguageItems { get; set; }

        public EshopDbContext(DbContextOptions options) : base(options)
        { 
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new OrderConfiguration());

            var entityTypes = builder.Model.GetEntityTypes();
            foreach (var entity in entityTypes)
            {
                entity.SetTableName(entity.GetTableName().Replace("AspNet", ""));
            }
        }
    }
}
