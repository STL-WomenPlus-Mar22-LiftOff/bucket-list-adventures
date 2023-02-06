using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BucketListAdventures.ViewModels
{
    public class SearchHotelsViewModel
    {
        [Required(ErrorMessage = "Gotta know where you wanna go.")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Gotta know when you wanna go.")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime? CheckIn { get; set; } = null;

        [Required(ErrorMessage = "How long do you plan on staying?")]
        public int NumOfNights { get; set; }

        [Required(ErrorMessage = "How many rooms do you need?")]
        public int NumOfRooms { get; set; }

        [Required(ErrorMessage = "How many adults are staying?")]
        public int NumOfAdults { get; set; }
        public int[] ChildAges { get; set; }


    }
}
