namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSalesOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalesOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sales", "SalesOrder_Id", c => c.Int());
            CreateIndex("dbo.Sales", "SalesOrder_Id");
            AddForeignKey("dbo.Sales", "SalesOrder_Id", "dbo.SalesOrders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "SalesOrder_Id", "dbo.SalesOrders");
            DropIndex("dbo.Sales", new[] { "SalesOrder_Id" });
            DropColumn("dbo.Sales", "SalesOrder_Id");
            DropTable("dbo.SalesOrders");
        }
    }
}
