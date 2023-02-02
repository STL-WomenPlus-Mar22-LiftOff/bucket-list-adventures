﻿using BucketListAdventures.Data;
using BucketListAdventures.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SearchActivities.ViewModel;
using System.Diagnostics;
using static BucketListAdventures.Models.ClimateNormals;

namespace BucketListAdventures.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserProfileRepository _repository;

 
        private readonly ILogger<HomeController> _logger;
        private static JArray data;

        ClimateNormals climateNormals = new ClimateNormals();
        private ApplicationRepository _repo;
        public HomeController(ILogger<HomeController> logger, ApplicationRepository repo, IUserProfileRepository repository)
        {
            _logger = logger;
            _repo = repo;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("/home/search")]
        public IActionResult Search()
        {
            SearchViewModel searchViewModel = new();
            return View(searchViewModel);
        }
        [HttpGet]
        [Route("/home/navigate")]
        public IActionResult Navigate()
        {
            SearchViewModel searchViewModel = new();
            return View(searchViewModel);
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
        public static async Task<JArray> GetActivities(double lon, double lat)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://travel-advisor.p.rapidapi.com/attractions/list-by-latlng?longitude={lon}&latitude={lat}&lunit=km&currency=USD&lang=en_US"),
                Headers =
    {
        { "X-RapidAPI-Key", "dce4b6271amshaa9de90c4bf28fdp144949jsn5e10c62a17bf" },
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
        public static async Task<JArray> GetNavigation(double lon, double lat)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.mapbox.com/directions/v5/mapbox/driving/-90.199585,38.626426;{lon},{lat}?geometries=geojson&access_token=pk.eyJ1IjoiY2hhbWFuZWJhcmJhdHRpIiwiYSI6ImNsY3FqcW9rZTA2aW4zcXBoMGx2eTBwNm0ifQ.LFRkBS7N5yGXvCQ_F5cF9g"),
            
               
            };
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            JObject value = JObject.Parse(body);
            
     
            
            data = (JArray)value["routes"];
            return data;
        }
        [HttpPost]
        [Route("/home/search")]
        public IActionResult DisplayResults(SearchViewModel searchViewModel)
        {
            Task<JObject> LatLong = GetLatLong(searchViewModel.CityName);
            JObject LatlongObject = LatLong.Result;
            double lon = (double)LatlongObject["features"][0]["geometry"]["coordinates"][0];
            double lat = (double)LatlongObject["features"][0]["geometry"]["coordinates"][1];
            Task<JArray> Activities = GetActivities(lon, lat);
            JArray activitiesObject = Activities.Result;

            WeatherStation closest_station = _repo.GetNearestWeatherStation(lat, lon);
            IEnumerable<MonthlyData> climateData = ReadCsvData(closest_station.station_id);

            ViewBag.activitiesObject = activitiesObject;

            ViewBag.climateData = climateData;

            return View();
        }
        [HttpPost]
        [Route("/home/navigate")]

        public IActionResult DisplayNavigate(SearchViewModel searchViewModel)
        {

            Task<JObject> LatLong = GetLatLong(searchViewModel.CityName);
            JObject LatlongObject = LatLong.Result;
            double lon = (double)LatlongObject["features"][0]["geometry"]["coordinates"][0];
            double lat = (double)LatlongObject["features"][0]["geometry"]["coordinates"][1];
            Task<JArray> Directions = GetNavigation(lon, lat);
            JArray directionsObject = Directions.Result;
            ViewBag.lon = lon;
            ViewBag.lat = lat;

           UserProfile userProfile = _repository.GetUserProfileByUserName(User.Identity.Name.ToString());
            if (userProfile != null)
            {
                ViewBag.Address = userProfile.Address;
                ViewBag.Name = userProfile.Name;
            }
            // Code for getting the address from the database goes here.
            
       
            string homeAddress = ViewBag.Address;
            
            Task<JObject> homeAddressLatLong = GetLatLong(homeAddress);
            JObject homeAddressLatlongObject = homeAddressLatLong.Result;
            double homeAddresslon = (double)homeAddressLatlongObject["features"][0]["geometry"]["coordinates"][0];
            double homeAddresslat = (double)homeAddressLatlongObject["features"][0]["geometry"]["coordinates"][1];
            
            
           
            ViewBag.homeAddresslon = homeAddresslon;
            ViewBag.homeAddresslat = homeAddresslat;
            ViewBag.directionsObject = directionsObject;


            return View();
        }
        [HttpGet]
        [Route("/home/details")]
        public IActionResult Details(string activity)
        {
            foreach (var activityDetail in data)
            {
                if (activity == (string)activityDetail["name"])
                {
                    ViewBag.activityDetails = activityDetail;
                    return View();
                }
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}