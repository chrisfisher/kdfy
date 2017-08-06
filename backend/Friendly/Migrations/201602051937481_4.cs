namespace Friendly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageLinks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.Location_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageLinks", "Location_Id", "dbo.Locations");
            DropIndex("dbo.ImageLinks", new[] { "Location_Id" });
            DropTable("dbo.ImageLinks");
        }
    }
}
