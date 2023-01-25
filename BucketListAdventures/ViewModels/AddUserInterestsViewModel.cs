namespace BucketListAdventures.ViewModels
{
    public class AddUserInterestsViewModel
    {
        public string UserInterest { get; set; }

        public bool IsSelected { get; set; }

        public AddUserInterestsViewModel(string userInterest, bool isSelected)
        {
            UserInterest = userInterest;
            IsSelected = isSelected;
        }

        public AddUserInterestsViewModel(string userInterest)
        {
            UserInterest=userInterest;
            IsSelected = false;
        }

        public AddUserInterestsViewModel()
        {

        }
    }
}