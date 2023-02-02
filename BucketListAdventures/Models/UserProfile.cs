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

        public string AirLineCode { get; set; }

        public UserProfile(string name, string username, string address, List<UserInterest> interests, string airLineCode) : this()
        {
            Name = name;
            UserName = username;
            Address = address;
            Interests = interests;
            AirLineCode = airLineCode;
        }

        public UserProfile()
        {
            Interests = new List<UserInterest>();   
        }
    }
}
