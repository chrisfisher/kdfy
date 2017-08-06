using System.Data.Entity;
using Friendly.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Friendly.Context
{
    public class FriendlyContext : IdentityDbContext<FriendlyUser>
    {
        public FriendlyContext() : base("FriendlyContext", throwIfV1Schema: false)
        {
        }

        public static FriendlyContext Create()
        {
            return new FriendlyContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .HasOptional(x => x.LocationType);
            
            modelBuilder.Entity<Location>()
                .Property(x => x.Latitude)
                .HasPrecision(12, 8);

            modelBuilder.Entity<Location>()
                .Property(x => x.Longitude)
                .HasPrecision(12, 8);

            modelBuilder.Entity<Location>()
                .HasMany(x => x.Reviews)
                .WithRequired();

            modelBuilder.Entity<LocationType>()
                .HasMany(x => x.Checks)
                .WithMany();

            modelBuilder.Entity<LocationType>()
                .HasMany(x => x.Ratings)
                .WithMany();

            modelBuilder.Entity<Check>()
                .HasOptional(x => x.Tag);

            modelBuilder.Entity<Rating>()
               .HasOptional(x => x.Tag);

            modelBuilder.Entity<LocationReview>()
                .HasMany(x => x.CheckScores)
                .WithMany();

            modelBuilder.Entity<LocationReview>()
                .HasMany(x => x.RatingScores)
                .WithMany();

            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationType> LocationTypes { get; set; }
        public virtual DbSet<Check> Checks { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<LocationReview> LocationScores { get; set; }
        public virtual DbSet<CheckScore> CheckScores { get; set; }
        public virtual DbSet<RatingScore> RatingScores { get; set; }
        public virtual DbSet<ImageLink> ImageLinks { get; set; }

    }
}