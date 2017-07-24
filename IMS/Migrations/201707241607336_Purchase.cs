namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Purchase : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Purchases", "VendorId");
            CreateIndex("dbo.Purchases", "ProductId");
            AddForeignKey("dbo.Purchases", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Purchases", "VendorId", "dbo.Vendors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Purchases", "ProductId", "dbo.Products");
            DropIndex("dbo.Purchases", new[] { "ProductId" });
            DropIndex("dbo.Purchases", new[] { "VendorId" });
        }
    }
}
