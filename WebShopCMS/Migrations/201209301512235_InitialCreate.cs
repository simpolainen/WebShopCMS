namespace WebShopCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            //if error:
            //Automatic migration was not applied because it would result in data loss.
            //Remove migrationhistory

            //CreateTable(
            //    "dbo.Products",
            //    c => new
            //        {
            //            ProductKey = c.Guid(nullable: false),
            //            Description = c.String(),
            //            Name = c.String(),
            //        })
            //    .PrimaryKey(t => t.ProductKey);
            //AddColumn("Products", "Test", c => c.String());
            //CreateTable(
            //  "dbo.Orders",
            //  c => new
            //  {
            //      OrderId = c.Guid(nullable: false),    
            //      Comment = c.String()
                  
                 
            //  })
            //  .PrimaryKey(t => t.OrderId);

            //AddForeignKey("Product_Order", "OrderId", "Orders");
            //AddForeignKey("Product_Order", "ProductKey", "Products");

            //CreateTable(
            //    "Order_Product",
            //    c => new
            //    {
            //        OrderId = c.Guid(nullable: false),
            //        ProductId = c.Guid(nullable: false)

            //    })
            //    .PrimaryKey(t => t.OrderId);
                
                

          

            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
