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
        public IActionResult Index()
        {
            return View();
        }

        //TODO: convert location to nearest weather station
        public IActionResult ClimateData(string stationId= "USC00040029")
        {
            IEnumerable<MonthlyData> data = GetClimateNormals(stationId);
            return View(data);
        }
    }
}


