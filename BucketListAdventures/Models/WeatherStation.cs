using System.ComponentModel.DataAnnotations;
using Microsoft.Spatial;

namespace BucketListAdventures.Models
{
    public class WeatherStation
    {
        //This is the primary key
        [Key]
        public int primary_id { get; set; }
        //This is the id that NOAA uses for the weather station
        public string station_id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        //elevation is in meters
        public double elevation { get; set; }
        public string station_name { get; set; }
        public GeographyPoint geography_point => GeographyPoint.Create(latitude, longitude);
    }
}
