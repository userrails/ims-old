namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsVatTaken = c.Boolean(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
           // AddColumn("dbo.Products", "SellingPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            // AddColumn("dbo.Products", "StockQty", c => c.Int(nullable: false));
           // AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            //AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
            //DropColumn("dbo.Products", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.Sales", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Sales", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Sales", "OrderId", "dbo.Orders");
            DropIndex("dbo.Sales", new[] { "OrderId" });
            DropIndex("dbo.Sales", new[] { "ProductId" });
            DropIndex("dbo.Sales", new[] { "VendorId" });
           // AlterColumn("dbo.Products", "Description", c => c.String());
            //AlterColumn("dbo.Products", "Name", c => c.String());
           // DropColumn("dbo.Products", "StockQty");
           // DropColumn("dbo.Products", "SellingPrice");
            DropTable("dbo.Sales");
            DropTable("dbo.Orders");
        }
    }
}
