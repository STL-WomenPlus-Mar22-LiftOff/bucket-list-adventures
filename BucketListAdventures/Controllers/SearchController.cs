using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucketListAdventures.Data;
using BucketListAdventures.Models;
using BucketListAdventures.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BucketListAdventures.Models.ClimateNormals;

namespace BucketListAdventures.Controllers
{
    public class SearchController : Controller
    {
        ClimateNormals climateNormals = new ClimateNormals();
        private ApplicationRepository _repo;

        public SearchController(ApplicationRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ClimateData(double latitude, double longitude)
        {
            WeatherStation closest_station = _repo.GetNearestWeatherStation(latitude, longitude);
            IEnumerable<MonthlyData> climateData = ReadCsvData(closest_station.station_id);
            return View(climateData);
        }
    }
}


