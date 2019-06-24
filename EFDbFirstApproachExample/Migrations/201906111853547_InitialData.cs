namespace EFDbFirstApproachExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Long(nullable: false, identity: true),
                        ProductName = c.String(),
                        Authors = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        DateOfPurpose = c.DateTime(),
                        AvailabilityStatus = c.String(),
                        CategoryID = c.Long(),
                        PublisherID = c.Long(),
                        Active = c.Boolean(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Publishers", t => t.PublisherID)
                .Index(t => t.CategoryID)
                .Index(t => t.PublisherID);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PublisherID = c.Long(nullable: false, identity: true),
                        PublisherName = c.String(),
                    })
                .PrimaryKey(t => t.PublisherID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "PublisherID", "dbo.Publishers");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "PublisherID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropTable("dbo.Publishers");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
