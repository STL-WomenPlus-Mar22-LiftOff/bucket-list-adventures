using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucketListAdventures.Data;
using BucketListAdventures.Models;
using BucketListAdventures.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BucketListAdventures.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository _repository;

        public UserProfileController(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        public IActionResult Index()
        {
            AddUserProfileViewModel viewModel = new AddUserProfileViewModel();
            string[] userInterests = { "Beaches", "Mountains", "Concerts", "Hiking", "Skiing", "Snorkeling", "Scuba Diving", "Amusement Parks", "Museums", "Zoos", "Water Sports", "Boat tours", "Historical Landmarks", "National Parks", "Nature Wildlife Areas", "Hidden Gems", "Adventurous", "Budget Friendly", "Good for Kids", "Bodies of Waters", "Water Parks", "Nightlife", "Events", "Shopping", "Wineries", "Golf", "Aquariums", "Botanical Gardens", "Gambling", "Haunted Tours" };
            UserProfile userProfile = _repository.GetUserProfileByUserName(User.Identity.Name.ToString());
            if (userProfile != null)
            {
                viewModel.Address = userProfile.Address;
                viewModel.Name = userProfile.Name;
            }
            viewModel.UserInterests = GetUserInterestsFromUserProfile(userProfile, userInterests);
            return View(viewModel);
        }

        [HttpPost()]
        public IActionResult AddUserProfile(AddUserProfileViewModel addUserProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfileToSave = _repository.GetUserProfileByUserName(User.Identity.Name.ToString());
                if (userProfileToSave == null)
                {
                    userProfileToSave = new UserProfile();
                    userProfileToSave.UserName = User.Identity.Name.ToString();
                    userProfileToSave.Address = addUserProfileViewModel.Address;
                    userProfileToSave.Name = addUserProfileViewModel.Name;
                    userProfileToSave.Interests = GetUserInterestsFromViewModel(addUserProfileViewModel);
                    _repository.AddUserProfile(userProfileToSave);
                }
                else
                {
                    userProfileToSave.Address = addUserProfileViewModel.Address;
                    userProfileToSave.Name = addUserProfileViewModel.Name;
                    userProfileToSave.Interests = GetUserInterestsFromViewModel(addUserProfileViewModel);
                    _repository.UpdateUserProfile(userProfileToSave);

                }
                _repository.SaveChages();
                return RedirectToAction("Index", "Home");
            }
            return View("Index", addUserProfileViewModel);
        }

        private List<UserInterest> GetUserInterestsFromViewModel(AddUserProfileViewModel addUserProfileViewModel)
        {
            List<UserInterest> userInterests = new List<UserInterest>();
            foreach (var item in addUserProfileViewModel.Interests)
            {
                userInterests.Add(new UserInterest(User.Identity.Name.ToString(), item));
            }
            return userInterests;
        }

        private List<AddUserInterestsViewModel> GetUserInterestsFromUserProfile(UserProfile userProfile, string[] userInterestsList)
        {
            List<AddUserInterestsViewModel> addUserInterestsViewModels = new List<AddUserInterestsViewModel>();
            if (userProfile != null && userProfile.Interests != null && userProfile.Interests.Count > 0)
            {
                foreach (var item in userInterestsList)
                {
                    addUserInterestsViewModels.Add(new AddUserInterestsViewModel(item, userProfile.Interests.Any(x => x.Interest.Equals(item))));
                }
                return addUserInterestsViewModels;
            }
            return (from userInterest in userInterestsList select new AddUserInterestsViewModel(userInterest)).ToList();
        }
    }
}
