using Bucket_List_Adventures.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SearchActivities.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bucket_List_Adventures.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static JArray data;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
            SearchViewModel searchViewModel = new SearchViewModel();
            return View(searchViewModel);
        }
        public static async Task<JObject> getLatLong(string city)
        {
            string accessToken = "pk.eyJ1IjoiY2hhbWFuZWJhcmJhdHRpIiwiYSI6ImNsY3FqcW9rZTA2aW4zcXBoMGx2eTBwNm0ifQ.LFRkBS7N5yGXvCQ_F5cF9g";
            HttpClient clientName = new HttpClient();
            string url = $"https://api.mapbox.com/geocoding/v5/mapbox.places/{city}.json?access_token={accessToken}";
            HttpResponseMessage responseName = await clientName.GetAsync(url);
            string responseString = await responseName.Content.ReadAsStringAsync();
            JObject position = JObject.Parse(responseString);
            return position;
        }
        public static async Task<JArray> getActivities(double lon,double lat)
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
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                JObject value = JObject.Parse(body);
                data = (JArray)value["data"];
                return data;
            }
        }
        [HttpPost]
        [Route("/home/search")]
        public IActionResult DisplayResults(SearchViewModel searchViewModel)
        {
            Task<JObject> LatLong = getLatLong(searchViewModel.cityName);
            JObject LatlongObject = LatLong.Result;
            double lon = (double)LatlongObject["features"][0]["geometry"]["coordinates"][0];
            double lat = (double)LatlongObject["features"][0]["geometry"]["coordinates"][1];
            Task<JArray> Activities = getActivities(lon, lat);
            JArray activitiesObject = Activities.Result;

            ViewBag.activitiesObject = activitiesObject;
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
