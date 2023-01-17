using BucketListAdventures.Models;

namespace BucketListAdventures.Data
{
    public interface IUserProfileRepository 
    {
        void SaveChages();
        UserProfile GetUserProfileByUserName(string userName);
        void AddUserProfile(UserProfile profile);
    }

    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserProfileDbContext _context;

        public UserProfileRepository()
        {

        }

        public UserProfileRepository(UserProfileDbContext context)
        {
            _context = context;

        }

        public void AddUserProfile(UserProfile profile)
        {
            _context.UserProfiles.Add(profile);
        }

        public UserProfile GetUserProfileByUserName(string userName)
        {
            return _context.UserProfiles.FirstOrDefault(x => x.UserName == userName);
        }

        public void SaveChages()
        {
            _context.SaveChanges();
        }
    }
}
