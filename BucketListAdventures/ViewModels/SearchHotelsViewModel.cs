using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BucketListAdventures.ViewModels
{
    public class SearchHotelsViewModel
    {
  
        public string CityName { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime? CheckIn { get; set; } = null;


        public int NumOfNights { get; set; }

        public int NumOfRooms { get; set; }

        public int NumOfAdults { get; set; }
        public int[] ChildAges { get; set; }


    }
}
