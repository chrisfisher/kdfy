namespace Friendly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Checks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Tag_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tags", t => t.Tag_Id)
                .Index(t => t.Tag_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CheckScores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckId = c.Int(nullable: false),
                        Value = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GooglePlaceId = c.String(),
                        GooglePlaceName = c.String(),
                        GooglePlaceAddress = c.String(),
                        AverageScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LocationType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocationTypes", t => t.LocationType_Id)
                .Index(t => t.LocationType_Id);
            
            CreateTable(
                "dbo.LocationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Tag_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tags", t => t.Tag_Id)
                .Index(t => t.Tag_Id);
            
            CreateTable(
                "dbo.LocationReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Score = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Location_Id = c.Int(),
                        Location_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id1, cascadeDelete: true)
                .Index(t => t.Location_Id)
                .Index(t => t.Location_Id1);
            
            CreateTable(
                "dbo.RatingScores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RatingId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        FriendlyUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.FriendlyUsers", t => t.FriendlyUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.FriendlyUser_Id);
            
            CreateTable(
                "dbo.FriendlyUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        FriendlyUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FriendlyUsers", t => t.FriendlyUser_Id)
                .Index(t => t.FriendlyUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        FriendlyUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.FriendlyUsers", t => t.FriendlyUser_Id)
                .Index(t => t.FriendlyUser_Id);
            
            CreateTable(
                "dbo.LocationTypeChecks",
                c => new
                    {
                        LocationType_Id = c.Int(nullable: false),
                        Check_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LocationType_Id, t.Check_Id })
                .ForeignKey("dbo.LocationTypes", t => t.LocationType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Checks", t => t.Check_Id, cascadeDelete: true)
                .Index(t => t.LocationType_Id)
                .Index(t => t.Check_Id);
            
            CreateTable(
                "dbo.LocationTypeRatings",
                c => new
                    {
                        LocationType_Id = c.Int(nullable: false),
                        Rating_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LocationType_Id, t.Rating_Id })
                .ForeignKey("dbo.LocationTypes", t => t.LocationType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Ratings", t => t.Rating_Id, cascadeDelete: true)
                .Index(t => t.LocationType_Id)
                .Index(t => t.Rating_Id);
            
            CreateTable(
                "dbo.LocationReviewCheckScores",
                c => new
                    {
                        LocationReview_Id = c.Int(nullable: false),
                        CheckScore_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LocationReview_Id, t.CheckScore_Id })
                .ForeignKey("dbo.LocationReviews", t => t.LocationReview_Id, cascadeDelete: true)
                .ForeignKey("dbo.CheckScores", t => t.CheckScore_Id, cascadeDelete: true)
                .Index(t => t.LocationReview_Id)
                .Index(t => t.CheckScore_Id);
            
            CreateTable(
                "dbo.LocationReviewRatingScores",
                c => new
                    {
                        LocationReview_Id = c.Int(nullable: false),
                        RatingScore_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LocationReview_Id, t.RatingScore_Id })
                .ForeignKey("dbo.LocationReviews", t => t.LocationReview_Id, cascadeDelete: true)
                .ForeignKey("dbo.RatingScores", t => t.RatingScore_Id, cascadeDelete: true)
                .Index(t => t.LocationReview_Id)
                .Index(t => t.RatingScore_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "FriendlyUser_Id", "dbo.FriendlyUsers");
            DropForeignKey("dbo.IdentityUserLogins", "FriendlyUser_Id", "dbo.FriendlyUsers");
            DropForeignKey("dbo.IdentityUserClaims", "FriendlyUser_Id", "dbo.FriendlyUsers");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.LocationReviews", "Location_Id1", "dbo.Locations");
            DropForeignKey("dbo.LocationReviewRatingScores", "RatingScore_Id", "dbo.RatingScores");
            DropForeignKey("dbo.LocationReviewRatingScores", "LocationReview_Id", "dbo.LocationReviews");
            DropForeignKey("dbo.LocationReviews", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.LocationReviewCheckScores", "CheckScore_Id", "dbo.CheckScores");
            DropForeignKey("dbo.LocationReviewCheckScores", "LocationReview_Id", "dbo.LocationReviews");
            DropForeignKey("dbo.Locations", "LocationType_Id", "dbo.LocationTypes");
            DropForeignKey("dbo.LocationTypeRatings", "Rating_Id", "dbo.Ratings");
            DropForeignKey("dbo.LocationTypeRatings", "LocationType_Id", "dbo.LocationTypes");
            DropForeignKey("dbo.Ratings", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.LocationTypeChecks", "Check_Id", "dbo.Checks");
            DropForeignKey("dbo.LocationTypeChecks", "LocationType_Id", "dbo.LocationTypes");
            DropForeignKey("dbo.Checks", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.LocationReviewRatingScores", new[] { "RatingScore_Id" });
            DropIndex("dbo.LocationReviewRatingScores", new[] { "LocationReview_Id" });
            DropIndex("dbo.LocationReviewCheckScores", new[] { "CheckScore_Id" });
            DropIndex("dbo.LocationReviewCheckScores", new[] { "LocationReview_Id" });
            DropIndex("dbo.LocationTypeRatings", new[] { "Rating_Id" });
            DropIndex("dbo.LocationTypeRatings", new[] { "LocationType_Id" });
            DropIndex("dbo.LocationTypeChecks", new[] { "Check_Id" });
            DropIndex("dbo.LocationTypeChecks", new[] { "LocationType_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "FriendlyUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "FriendlyUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "FriendlyUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.LocationReviews", new[] { "Location_Id1" });
            DropIndex("dbo.LocationReviews", new[] { "Location_Id" });
            DropIndex("dbo.Ratings", new[] { "Tag_Id" });
            DropIndex("dbo.Locations", new[] { "LocationType_Id" });
            DropIndex("dbo.Checks", new[] { "Tag_Id" });
            DropTable("dbo.LocationReviewRatingScores");
            DropTable("dbo.LocationReviewCheckScores");
            DropTable("dbo.LocationTypeRatings");
            DropTable("dbo.LocationTypeChecks");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.FriendlyUsers");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.RatingScores");
            DropTable("dbo.LocationReviews");
            DropTable("dbo.Ratings");
            DropTable("dbo.LocationTypes");
            DropTable("dbo.Locations");
            DropTable("dbo.CheckScores");
            DropTable("dbo.Tags");
            DropTable("dbo.Checks");
        }
    }
}
