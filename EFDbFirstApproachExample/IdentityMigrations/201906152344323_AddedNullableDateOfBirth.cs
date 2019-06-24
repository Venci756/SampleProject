namespace EFDbFirstApproachExample.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNullableDateOfBirth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime(nullable: false));
        }
    }
}
