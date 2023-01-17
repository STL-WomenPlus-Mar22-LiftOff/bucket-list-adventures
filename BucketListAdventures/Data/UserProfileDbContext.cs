using BucketListAdventures.Models;
using Microsoft.EntityFrameworkCore;

namespace BucketListAdventures.Data
{
    public class UserProfileDbContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }

        public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options)
: base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
     }
}
