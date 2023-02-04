using Microsoft.AspNetCore.Mvc;
using BucketListAdventures.ViewModels;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using BucketListAdventures.Models;
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

        public static async Task<JObject> HotelList(double lon, double lat)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://travel-advisor.p.rapidapi.com/hotels/v2/list?"),
                Headers =
    {
        { "X-RapidAPI-Key", "0d2cab3ae3mshb44fd77664469a0p1c8f19jsn43b9ff0d5a7f" },
        { "X-RapidAPI-Host", "travel-advisor.p.rapidapi.com" },
    },
                Content = new StringContent("{\"geoId\":29392,\"checkIn\":\"2022-03-10\",\"checkOut\":\"2022-03-15\",\"sort\":\"PRICE_LOW_TO_HIGH\",\"sortOrder\":\"asc\",\"filters\":[{\"id\":\"deals\",\"value\":[\"1\",\"2\",\"3\"]},{\"id\":\"price\",\"value\":[\"31\",\"122\"]},{\"id\":\"type\",\"value\":[\"9189\",\"9201\"]},{\"id\":\"amenity\",\"value\":[\"9156\",\"9658\",\"21778\",\"9176\"]},{\"id\":\"distFrom\",\"value\":[\"2227712\",\"25.0\"]},{\"id\":\"rating\",\"value\":[\"40\"]},{\"id\":\"class\",\"value\":[\"9572\"]}],\"rooms\":[{\"adults\":2,\"childrenAges\":[2]},{\"adults\":2,\"childrenAges\":[3]}],\"boundingBox\":{\"northEastCorner\":{\"latitude\":12.248278039408776,\"longitude\":109.1981618106365},\"southWestCorner\":{\"latitude\":12.243407232845051,\"longitude\":109.1921640560031}},\"updateToken\":\"\"}")
            };
        }

        


      

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
             return (JObject)body;
             }
        }
    }
} 

