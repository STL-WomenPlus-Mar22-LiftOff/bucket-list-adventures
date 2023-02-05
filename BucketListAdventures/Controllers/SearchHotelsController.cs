using BucketListAdventures.Models;
using BucketListAdventures.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SearchHotels.ViewModel;
using System.Diagnostics;



namespace BucketListAdventures.Controllers
{
    public class SearchHotelsController : Controller
    {

        private readonly ILogger<SearchHotelsController> _logger;
        private static JArray data;
        public SearchHotelsController(ILogger<SearchHotelsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/home/hotel")]
        public IActionResult Search()
        {
            SearchHotelsViewModel searchHotelsViewModel = new();
            return View(searchHotelsViewModel);
        }
        public static async Task<JObject> GetLatLong(string city)
        {
            string accessToken = "pk.eyJ1IjoiY2hhbWFuZWJhcmJhdHRpIiwiYSI6ImNsY3FqcW9rZTA2aW4zcXBoMGx2eTBwNm0ifQ.LFRkBS7N5yGXvCQ_F5cF9g";
            HttpClient clientName = new();
            string url = $"https://api.mapbox.com/geocoding/v5/mapbox.places/{city}.json?access_token={accessToken}";
            HttpResponseMessage responseName = await clientName.GetAsync(url);
            string responseString = await responseName.Content.ReadAsStringAsync();
            JObject position = JObject.Parse(responseString);
            return position;
        }

        public static async Task<JArray> GetHotels(double lat, double lon)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://travel-advisor.p.rapidapi.com/hotels/list-by-latlng?latitude={lat}&longitude={lon}&lang=en_US&currency=USD"),
                Headers =
                {
                    { "X-RapidAPI-Key", "293fcdc097mshb921a2ca7278e53p12a2e5jsnc86a94d37c17" },
                    { "X-RapidAPI-Host", "travel-advisor.p.rapidapi.com" },
                },
            };
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            JObject value = JObject.Parse(body);
            data = (JArray)value["data"];
            return data;
        }

        [HttpPost]
        [Route("/home/hotel")]
        public IActionResult DisplayHotelResults(SearchHotelsViewModel searchHotelsViewModel)
        {
            Task<JObject> LatLong = GetLatLong(searchHotelsViewModel.CityName);
            JObject LatlongObject = LatLong.Result;
            double lon = (double)LatlongObject["features"][0]["geometry"]["coordinates"][0];
            double lat = (double)LatlongObject["features"][0]["geometry"]["coordinates"][1];
            Task<JArray> Hotels = GetHotels(lon, lat);
            JArray hotelsObject = Hotels.Result;

            ViewBag.hotelsObject = hotelsObject;

            return View();
        }
        [HttpGet]
        [Route("/hotel/details")]
        public IActionResult DisplayHotelDetails(string hotel)
        {
            foreach (var hotelDetail in data)
            {
                if (hotel == (string)hotelDetail["name"])
                {
                    ViewBag.hotelDetails = hotelDetail;
                    return View();
                }
            }
            return View();
        }
    }
} 

