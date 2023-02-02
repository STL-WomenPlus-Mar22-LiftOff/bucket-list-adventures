﻿using Microsoft.AspNetCore.Mvc;
using BucketListAdventures.Data;
using BucketListAdventures.Models;
using BucketListAdventures.ViewModels;
using amadeus;
using SearchActivities.ViewModel;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using amadeus.exceptions;
using amadeus.resources;
using Humanizer;
using System.Reflection.PortableExecutable;
using DotLiquid.Util;
using System.Net.Http.Headers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BucketListAdventures.Controllers
{
    public class SearchHotelsController : Controller
    {

        private readonly ILogger<SearchHotelsController> _logger;
        private static JArray data;
        private static object childAges;
        private static object numOfAdults;
        private static object checkinDate;
        private static object numOfNights;
        private static object amenitites;
        private static object numOfRooms;
        private static object hotelClass;

        public SearchHotelsController(ILogger<SearchHotelsController> logger)
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
        [Route("/home/hotel")]
        public IActionResult Search()
        {
            SearchHotelsViewModel searchHotelsViewModel = new SearchHotelsViewModel();
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

        public static async Task<JObject> HotelListByLatLog(double lon, double lat)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://travel-advisor.p.rapidapi.com/hotels/list-by-latlng?latitude={lat}&longitude={lon}&lang=en_US&hotel_class={hotelClass}&limit=50&adults={numOfAdults}&amenities={amenitites}&rooms={numOfRooms}&child_rm_ages={childAges}&checkin={checkinDate}&nights={numOfNights}"),
                Headers =
            {
                { "X-RapidAPI-Key", "293fcdc097mshb921a2ca7278e53p12a2e5jsnc86a94d37c17" },
                { "X-RapidAPI-Host", "travel-advisor.p.rapidapi.com" },
            },
                };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return (JObject)body;
            }
        }


        [HttpPost]
        [Route("/home/hotel")]
        public async Task<IActionResult> DisplayHotelList(SearchHotelsViewModel searchHotelsViewModel)
        {


            var client = new HttpClient();
            var request = new HttpRequestMessage

            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://travel-advisor.p.rapidapi.com/hotel-filters/v2/list?lang=en_US&units=mi&currency=USD"),
                Headers =
    {
        { "X-RapidAPI-Key", "293fcdc097mshb921a2ca7278e53p12a2e5jsnc86a94d37c17" },
        { "X-RapidAPI-Host", "travel-advisor.p.rapidapi.com" },
    },
                Content = new StringContent("{\"geoId\": 293928,\"checkIn\":\"2021-07-03\",\"checkOut\":\"2021-07-05\",\"sort\":\"PRICE_LOW_TO_HIGH\",\"sortOrder\":\"asc\",\"filters\":[{\"id\":\"deals\",\"value\":[\"1\",\"2\",\"3\"]},{\"id\":\"price\",\"value\":[\"31\",\"122\"]},{\"id\":\"type\",\"value\":[\"9189\",\"9201\"]},{\"id\":\"amenity\",\"value\":[\"9156\",\"9658\",\"21778\",\"9176\"]},{\"id\":\"distFrom\",\"value\":[\"2227712\",\"25.0\"]},{\"id\":\"rating\",\"value\":[\"40\"]},{\"id\":\"class\",\"value\":[\"9572\"]}],\"rooms\":[{\"adults\":2,\"childrenAges\":[2]},{\"adults\":2,\"childrenAges\":[3]}]}")

    {
                Headers =

        {
                    ContentType = new MediaTypeHeaderValue("application/json")

        }
            }
        };
            using (var response = await client.SendAsync(request))
{
            	response.EnsureSuccessStatusCode();
	            var body = await response.Content.ReadAsStringAsync();
                ViewBag.body = body;

                return View();
            }
        }

    }
} 

