using System.ComponentModel.DataAnnotations;

namespace BucketListAdventures.Models
{
    public class UserInterest
    {
        [Key]
        public int Id { get; set; }

        public string Interest { get; set; }

        public UserProfile UserProfile { get; set; }

        public string UserProfileUserName { get; set; }

        public UserInterest()
        {

        }

        public UserInterest(string userName, string userInterest)
        {
            UserProfileUserName = userName;
            Interest = userInterest;
        }
    }
}