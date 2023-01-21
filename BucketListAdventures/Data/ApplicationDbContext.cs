using BucketListAdventures.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BucketListAdventures.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Destination> Destinations { get; set; }
        
        public DbSet<WeatherStation> WeatherStations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //this property is not in the table and is only created in the class
            //so it should be excluded from mapping
            modelBuilder.Entity<WeatherStation>().Ignore(x => x.geography_point);
        }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
