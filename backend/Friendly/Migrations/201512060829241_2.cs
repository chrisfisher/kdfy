namespace Friendly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "LocalSecrets", c => c.String());
            AddColumn("dbo.LocationReviews", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LocationReviews", "Comment");
            DropColumn("dbo.Locations", "LocalSecrets");
        }
    }
}
