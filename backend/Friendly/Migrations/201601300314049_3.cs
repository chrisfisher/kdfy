namespace Friendly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "Latitude", c => c.Decimal(nullable: false, precision: 12, scale: 8));
            AlterColumn("dbo.Locations", "Longitude", c => c.Decimal(nullable: false, precision: 12, scale: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Locations", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
