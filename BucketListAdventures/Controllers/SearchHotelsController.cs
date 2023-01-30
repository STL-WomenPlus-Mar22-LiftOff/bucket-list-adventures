using Microsoft.AspNetCore.Mvc;
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


namespace BucketListAdventures.Controllers
{
    public class SearchHotelsController : Controller
    {
        public static FindAHotel(string cityName)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://travel-advisor.p.rapidapi.com/locations/v2/auto-complete?query=eiffel%20tower&lang=en_US&units=mi"),
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
                Console.WriteLine(body);
            }
        }
    } 
}   
