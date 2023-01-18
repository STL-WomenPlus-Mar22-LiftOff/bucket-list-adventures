using BucketListAdventures.Models;

namespace BucketListAdventures.Data
{
    public interface IUserProfileRepository 
    {
        void SaveChages();
        UserProfile GetUserProfileByUserName(string userName);
        void AddUserProfile(UserProfile profile);
        void UpdateUserProfile(UserProfile profile);
    }

    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository()
        {

        }

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public void AddUserProfile(UserProfile profile)
        {
            _context.UserProfiles.Add(profile);
        }

        public void UpdateUserProfile(UserProfile profile)
        {
            UserProfile userProfile = GetUserProfileByUserName(profile.UserName);
            userProfile.Address = profile.Address;
            userProfile.Interests = profile.Interests;
            userProfile.Name = profile.Name;
            _context.UserProfiles.Update(userProfile);
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
