namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sales : DbMigration
    {
        public override void Up()
        {
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
                    SalesOrderId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .ForeignKey("dbo.SalesOrders", t => t.SalesOrderId, cascadeDelete: true)
                .Index(t => t.VendorId)
                .Index(t => t.ProductId)
                .Index(t => t.SalesOrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Sales", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Sales", "SalesOrderId", "dbo.SalesOrders");
            DropIndex("dbo.Sales", new[] { "ProductId" });
            DropIndex("dbo.Sales", new[] { "VendorId" });
            DropIndex("dbo.sales", new[] { "SalesOrderId" });
            DropTable("dbo.Sales");
        }
    }
}
