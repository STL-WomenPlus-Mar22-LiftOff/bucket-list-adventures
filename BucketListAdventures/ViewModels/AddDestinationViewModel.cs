using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BucketListAdventures.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BucketListAdventures.ViewModels
{
    public class AddDestinationViewModel
    {
        [Required(ErrorMessage = "Name of your destination spot is required.")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location of your destination is required.")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Location must be between 3 and 50 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please enter a description of your destination spot.")]
        [StringLength(3000, ErrorMessage = "Description is too long!")]
        public string Description { get; set; }

        public string? LocationId { get; set; }

        public AddDestinationViewModel() { }
    }
}
