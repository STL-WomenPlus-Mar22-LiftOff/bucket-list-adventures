using System.ComponentModel.DataAnnotations;

namespace BucketListAdventures.ViewModels
{
    public class AddUserProfileViewModel
    {
        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Address is required")]
        public string Address { get; set; }
        
        public List<AddUserInterestsViewModel> UserInterests { get; set; }

        [Required(ErrorMessage = "Interests is required")]
        public List<string> Interests { get; set; }

        [Required(ErrorMessage = "AirLine code is required")]
        public string AirLineCode { get; set; }

        public AddUserProfileViewModel()
        {
            UserInterests = new List<AddUserInterestsViewModel>();
        }
    }
}
