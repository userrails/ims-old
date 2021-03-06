namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Purchase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
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
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId)
                .Index(t => t.ProductId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Purchases", "ProductId", "dbo.Products");
            DropIndex("dbo.Purchases", new[] { "ProductId" });
            DropIndex("dbo.Purchases", new[] { "VendorId" });
            DropTable("dbo.Purchases");
        }
    }
}
