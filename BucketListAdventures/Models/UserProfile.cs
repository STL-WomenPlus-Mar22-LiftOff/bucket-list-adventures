using System.ComponentModel.DataAnnotations;

namespace BucketListAdventures.Models
{
    public class UserProfile
    {
        public string Name { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Address { get; set; }
        public List<UserInterest> Interests { get; set; }

        public UserProfile(string name, string username, string address, List<UserInterest> interests) : this()
        {
            Name = name;
            UserName = username;
            Address = address;
            Interests = interests;
        }

        public UserProfile()
        {
            Interests = new List<UserInterest>();   
        }
    }
}
