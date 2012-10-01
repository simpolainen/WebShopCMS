using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebShopCMS.Models
{
    public class WebShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().
              HasMany(p => p.Products).
              WithMany(o => o.Orders).
              Map(
               m =>
               {
                   m.MapLeftKey("Order_OrderId");
                   m.MapRightKey("Product_ProductKey");
                   m.ToTable("OrderProducts");
               });
        }
    }
}