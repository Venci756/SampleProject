namespace EFDbFirstApproachExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingNullColumns : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Products", "PublisherID", "dbo.Publishers");
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "PublisherID" });
            AlterColumn("dbo.Products", "CategoryID", c => c.Long(nullable: false));
            AlterColumn("dbo.Products", "PublisherID", c => c.Long(nullable: false));
            AlterColumn("dbo.Products", "Active", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Products", "CategoryID");
            CreateIndex("dbo.Products", "PublisherID");
            AddForeignKey("dbo.Products", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
            AddForeignKey("dbo.Products", "PublisherID", "dbo.Publishers", "PublisherID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "PublisherID", "dbo.Publishers");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "PublisherID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            AlterColumn("dbo.Products", "Active", c => c.Boolean());
            AlterColumn("dbo.Products", "PublisherID", c => c.Long());
            AlterColumn("dbo.Products", "CategoryID", c => c.Long());
            CreateIndex("dbo.Products", "PublisherID");
            CreateIndex("dbo.Products", "CategoryID");
            AddForeignKey("dbo.Products", "PublisherID", "dbo.Publishers", "PublisherID");
            AddForeignKey("dbo.Products", "CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}
