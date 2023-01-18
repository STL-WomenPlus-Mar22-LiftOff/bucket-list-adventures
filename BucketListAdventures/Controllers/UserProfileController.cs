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
            UserProfile userProfile = _repository.GetUserProfileByUserName(User.Identity.Name.ToString());
            if (userProfile != null)
            {
                viewModel.Address = userProfile.Address;
                viewModel.Name = userProfile.Name;
                viewModel.Interests = userProfile.Interests;
            }
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
                    userProfileToSave.Interests = addUserProfileViewModel.Interests;
                    _repository.AddUserProfile(userProfileToSave);
                }
                else
                {
                    userProfileToSave.Address = addUserProfileViewModel.Address;
                    userProfileToSave.Name = addUserProfileViewModel.Name;
                    userProfileToSave.Interests = addUserProfileViewModel.Interests;
                    _repository.UpdateUserProfile(userProfileToSave);

                }
                _repository.SaveChages();
                return RedirectToAction("Index", "Home");
            }
            return View("Index", addUserProfileViewModel);
        }
    }
}
